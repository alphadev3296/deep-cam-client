"""
Camera Management Module
Handles camera detection, selection, and frame capture
"""

import threading
import time
from collections.abc import Callable
from typing import Any

import cv2
import numpy as np
from loguru import logger


class CameraManager:
    """Manages camera operations including detection, streaming, and capture"""

    def __init__(self) -> None:
        self.current_camera: cv2.VideoCapture | None = None
        self.camera_index = -1
        self.is_streaming = False
        self.stream_thread = None
        self.frame_callback = None
        self.latest_frame = None
        self.frame_lock = threading.Lock()

    def get_available_cameras(self) -> list[dict[str, Any]]:
        """
        Detect and return list of available cameras
        Returns list of dicts with 'index' and 'name' keys
        """
        cameras = []

        # Test camera indices 0-10 (should cover most systems)
        for i in range(10):
            cap = cv2.VideoCapture(i)
            if cap.isOpened():
                # Try to read a frame to verify camera works
                ret, _ = cap.read()
                if ret:
                    # Get camera name/info if available
                    name = f"Camera {i}"
                    try:
                        # Try to get more descriptive name
                        backend = cap.getBackendName()
                        width = int(cap.get(cv2.CAP_PROP_FRAME_WIDTH))
                        height = int(cap.get(cv2.CAP_PROP_FRAME_HEIGHT))
                        name = f"Camera {i} ({backend}) - {width}x{height}"
                    except:  # noqa: E722, S110
                        pass

                    cameras.append({"index": i, "name": name})
                cap.release()

        return cameras

    def select_camera(self, camera_index: int) -> bool:
        """
        Select and initialize a camera by index
        Returns True if successful, False otherwise
        """
        try:
            # Release current camera if any
            self.stop_streaming()
            if self.current_camera:
                self.current_camera.release()

            # Open new camera
            self.current_camera = cv2.VideoCapture(camera_index)
            if not self.current_camera.isOpened():
                return False

            # Set optimal settings
            self.current_camera.set(cv2.CAP_PROP_FRAME_WIDTH, 640)
            self.current_camera.set(cv2.CAP_PROP_FRAME_HEIGHT, 480)
            self.current_camera.set(cv2.CAP_PROP_FPS, 30)

            self.camera_index = camera_index
            return True  # noqa: TRY300

        except Exception as e:
            logger.exce(f"Error selecting camera {camera_index}: {e}")
            return False

    def start_streaming(self, frame_callback: Callable[[np.ndarray], None]) -> bool:
        """
        Start streaming from the selected camera
        frame_callback: Function to call with each frame
        """
        if not self.current_camera or not self.current_camera.isOpened():
            return False

        if self.is_streaming:
            return True

        self.frame_callback = frame_callback
        self.is_streaming = True
        self.stream_thread = threading.Thread(target=self._stream_worker, daemon=True)
        self.stream_thread.start()
        return True

    def stop_streaming(self) -> None:
        """Stop the camera stream"""
        self.is_streaming = False
        if self.stream_thread and self.stream_thread.is_alive():
            self.stream_thread.join(timeout=1.0)

    def _stream_worker(self) -> None:
        """Worker thread for camera streaming"""
        while self.is_streaming and self.current_camera and self.current_camera.isOpened():
            ret, frame = self.current_camera.read()
            if ret and frame is not None:
                # Store latest frame for capture
                with self.frame_lock:
                    self.latest_frame = frame.copy()

                # Call the callback with the frame
                if self.frame_callback:
                    try:
                        self.frame_callback(frame)
                    except Exception as e:
                        logger.exception(f"Error in frame callback: {e}")
            else:
                # If we can't read frames, wait a bit and try again
                time.sleep(0.1)

        logger.info("Camera stream worker stopped")

    def capture_frame(self, filename: str | None = None) -> str | None:
        """
        Capture the current frame and save it
        Returns the filename if successful, None otherwise
        """
        with self.frame_lock:
            if self.latest_frame is None:
                return None

            if filename is None:
                timestamp = time.strftime("%Y%m%d_%H%M%S")
                filename = f"capture_{timestamp}.jpg"

            try:
                success = cv2.imwrite(filename, self.latest_frame)
                return filename if success else None  # noqa: TRY300
            except Exception as e:
                logger.exception(f"Error saving capture: {e}")
                return None

    def get_current_frame(self) -> np.ndarray | None:
        """Get the latest frame as numpy array"""
        with self.frame_lock:
            return self.latest_frame.copy() if self.latest_frame is not None else None

    def release(self) -> None:
        """Clean up camera resources"""
        self.stop_streaming()
        if self.current_camera:
            self.current_camera.release()
            self.current_camera = None

    def __del__(self) -> None:
        """Destructor to ensure cleanup"""
        self.release()
