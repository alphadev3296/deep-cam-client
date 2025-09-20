import asyncio

import aiohttp
import cv2
from aiortc import MediaStreamTrack, RTCPeerConnection, RTCSessionDescription, VideoStreamTrack
from av import VideoFrame

from app.core.env import config as cfg_env


# Video capture + preprocessing track
class CameraVideoTrack(VideoStreamTrack):
    def __init__(self, camera_index: int = 1) -> None:
        super().__init__()
        self.cap = cv2.VideoCapture(camera_index)

    async def recv(self) -> VideoFrame:
        pts, time_base = await self.next_timestamp()
        ret, frame = self.cap.read()
        if not ret:
            return None

        # Preprocess (example: flip horizontally)
        frame = cv2.flip(frame, 1)

        new_frame = VideoFrame.from_ndarray(frame, format="bgr24")
        new_frame.pts = pts
        new_frame.time_base = time_base
        return new_frame


async def run_client(server_url: str) -> None:
    pc = RTCPeerConnection()
    local_video = CameraVideoTrack()
    pc.addTrack(local_video)

    @pc.on("track")
    def on_track(track: MediaStreamTrack) -> None:
        if track.kind == "video":
            asyncio.ensure_future(display(track=track))  # noqa: RUF006

    offer = await pc.createOffer()
    await pc.setLocalDescription(offer)

    async with (
        aiohttp.ClientSession() as session,
        session.post(server_url, json={"sdp": pc.localDescription.sdp, "type": pc.localDescription.type}) as resp,
    ):
        answer = await resp.json()

    await pc.setRemoteDescription(RTCSessionDescription(sdp=answer["sdp"], type=answer["type"]))

    await asyncio.Future()


async def display(track: VideoStreamTrack) -> None:
    try:
        while True:
            frame = await track.recv()
            img = frame.to_ndarray(format="bgr24")

            # Postprocess (example: draw label)
            cv2.putText(img, "Processed Frame", (20, 40), cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 255, 0), 2)

            cv2.imshow("Client Video", img)
            if cv2.waitKey(1) & 0xFF == ord("q"):
                break
    finally:
        cv2.destroyAllWindows()


if __name__ == "__main__":
    asyncio.run(
        run_client(
            server_url=cfg_env.SERVER_URL,
        )
    )
