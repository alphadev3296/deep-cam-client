"""
Main GUI Window
Tkinter-based interface for camera streaming application
"""

import os
import tkinter as tk
from tkinter import filedialog, messagebox, ttk

import cv2
import numpy as np
from loguru import logger
from PIL import Image, ImageTk

from app.service.camera_manager import CameraManager


class CameraApp:
    """Main camera application GUI"""

    def __init__(self) -> None:
        self.root = tk.Tk()
        self.root.title("Camera Stream Application")
        self.root.geometry("900x700")
        self.root.protocol("WM_DELETE_WINDOW", self.on_closing)

        # Core components
        self.camera_manager = CameraManager()

        # GUI state variables
        self.selected_camera_var = tk.StringVar()
        self.streaming_var = tk.BooleanVar()
        self.local_preview_var = tk.BooleanVar(value=True)
        self.status_var = tk.StringVar(value="Ready")
        self.client_count_var = tk.StringVar(value="0")

        # Display variables
        self.current_image_label = None
        self.photo_image = None

        # Setup GUI
        self.setup_gui()
        self.refresh_camera_list()

        # Start status update timer
        self.update_status()

    def setup_gui(self) -> None:
        """Setup the GUI layout"""
        # Main container with padding
        main_frame = ttk.Frame(self.root, padding="10")
        main_frame.grid(row=0, column=0, sticky=tk.NSEW)

        # Configure grid weights
        self.root.columnconfigure(0, weight=1)
        self.root.rowconfigure(0, weight=1)
        main_frame.columnconfigure(1, weight=1)
        main_frame.rowconfigure(0, weight=1)
        main_frame.rowconfigure(1, weight=1)

        # Left panel - controls
        self.setup_control_panel(main_frame)

        # Right panel - video display
        self.setup_display_panel(main_frame)

        # Bottom panel - status and info
        self.setup_status_panel(main_frame)

    def setup_control_panel(self, parent: ttk.Frame) -> None:
        """Setup the left control panel"""
        control_frame = ttk.LabelFrame(parent, text="Controls", padding="10")
        control_frame.grid(row=0, column=0, rowspan=2, sticky=tk.NSEW, padx=(0, 10))

        row = 0

        # Camera Selection
        ttk.Label(control_frame, text="Camera:").grid(row=row, column=0, sticky=tk.W, pady=(0, 5))
        row += 1

        self.camera_combo = ttk.Combobox(
            control_frame, textvariable=self.selected_camera_var, state="readonly", width=25
        )
        self.camera_combo.grid(row=row, column=0, columnspan=2, sticky=tk.EW, pady=(0, 5))
        self.camera_combo.bind("<<ComboboxSelected>>", self.on_camera_selected)
        row += 1

        # Refresh cameras button
        ttk.Button(control_frame, text="Refresh Cameras", command=self.refresh_camera_list).grid(
            row=row, column=0, columnspan=2, sticky=tk.EW, pady=(0, 10)
        )
        row += 1

        # Separator
        ttk.Separator(control_frame, orient="horizontal").grid(row=row, column=0, columnspan=2, sticky=tk.EW, pady=10)
        row += 1

        # Photo Selection
        ttk.Label(control_frame, text="Static Image:").grid(row=row, column=0, sticky=tk.W, pady=(0, 5))
        row += 1

        ttk.Button(control_frame, text="Choose Photo", command=self.choose_photo).grid(
            row=row, column=0, columnspan=2, sticky=tk.EW, pady=(0, 10)
        )
        row += 1

        # Separator
        ttk.Separator(control_frame, orient="horizontal").grid(row=row, column=0, columnspan=2, sticky=tk.EW, pady=10)
        row += 1

        # Streaming Controls
        ttk.Label(control_frame, text="Streaming:").grid(row=row, column=0, sticky=tk.W, pady=(0, 5))
        row += 1

        # Start/Stop streaming buttons
        button_frame = ttk.Frame(control_frame)
        button_frame.grid(row=row, column=0, columnspan=2, sticky=tk.EW, pady=(0, 5))
        button_frame.columnconfigure(0, weight=1)
        button_frame.columnconfigure(1, weight=1)

        self.start_btn = ttk.Button(button_frame, text="Start Stream", command=self.start_streaming)
        self.start_btn.grid(row=0, column=0, sticky=tk.EW, padx=(0, 5))

        self.stop_btn = ttk.Button(button_frame, text="Stop Stream", command=self.stop_streaming, state="disabled")
        self.stop_btn.grid(row=0, column=1, sticky=tk.EW)
        row += 1

        # Capture button
        ttk.Button(control_frame, text="Capture Frame", command=self.capture_frame).grid(
            row=row, column=0, columnspan=2, sticky=tk.EW, pady=(5, 10)
        )
        row += 1

        # Local preview checkbox
        ttk.Checkbutton(control_frame, text="Local Preview", variable=self.local_preview_var).grid(
            row=row, column=0, columnspan=2, sticky=tk.W, pady=(0, 10)
        )
        row += 1

        # Remote connection info
        self.remote_info_label = ttk.Label(control_frame, text="", foreground="blue", cursor="hand2")
        self.remote_info_label.grid(row=row, column=0, columnspan=2, sticky=tk.W, pady=(5, 0))

        # Configure column weights
        control_frame.columnconfigure(0, weight=1)

    def setup_display_panel(self, parent: ttk.Frame) -> None:
        """Setup the right display panel"""
        display_frame = ttk.LabelFrame(parent, text="Video Display", padding="10")
        display_frame.grid(row=0, column=1, rowspan=2, sticky=tk.NSEW)

        # Configure display frame
        display_frame.columnconfigure(0, weight=1)
        display_frame.rowconfigure(0, weight=1)

        # Create canvas for video display
        self.video_canvas = tk.Canvas(display_frame, bg="black", width=640, height=480)
        self.video_canvas.grid(row=0, column=0, sticky=tk.NSEW)

        # Add scrollbars (in case image is larger than canvas)
        v_scrollbar = ttk.Scrollbar(display_frame, orient="vertical", command=self.video_canvas.yview)
        v_scrollbar.grid(row=0, column=1, sticky=tk.NS)

        h_scrollbar = ttk.Scrollbar(display_frame, orient="horizontal", command=self.video_canvas.xview)
        h_scrollbar.grid(row=1, column=0, sticky=tk.EW)

        self.video_canvas.configure(yscrollcommand=v_scrollbar.set, xscrollcommand=h_scrollbar.set)

    def setup_status_panel(self, parent: ttk.Frame) -> None:
        """Setup the bottom status panel"""
        status_frame = ttk.Frame(parent)
        status_frame.grid(row=2, column=0, columnspan=2, sticky=tk.EW, pady=(10, 0))
        status_frame.columnconfigure(0, weight=1)

        # Status label
        ttk.Label(status_frame, text="Status:").grid(row=0, column=0, sticky=tk.W)
        ttk.Label(status_frame, textvariable=self.status_var).grid(row=0, column=1, sticky=tk.W, padx=(10, 0))

    def refresh_camera_list(self) -> None:
        """Refresh the list of available cameras"""
        try:
            cameras = self.camera_manager.get_available_cameras()
            camera_names = [cam["name"] for cam in cameras]

            self.camera_combo["values"] = camera_names
            self.camera_list = cameras  # Store for index lookup

            if cameras:
                self.camera_combo.current(0)
                self.status_var.set(f"Found {len(cameras)} camera(s)")
            else:
                self.status_var.set("No cameras found")

        except Exception as e:
            messagebox.showerror("Error", f"Failed to refresh cameras: {e}")
            self.status_var.set("Error refreshing cameras")

    def on_camera_selected(self, event=None) -> None:  # noqa: ANN001, ARG002
        """Handle camera selection"""
        selection = self.camera_combo.current()
        if selection >= 0 and selection < len(self.camera_list):
            camera_info = self.camera_list[selection]
            success = self.camera_manager.select_camera(camera_info["index"])
            if success:
                self.status_var.set(f"Selected: {camera_info['name']}")
            else:
                messagebox.showerror("Error", f"Failed to select camera: {camera_info['name']}")
                self.status_var.set("Camera selection failed")

    def choose_photo(self) -> None:
        """Choose and display a static photo"""
        filetypes = [
            ("Image files", "*.jpg *.jpeg *.png *.bmp *.tiff *.gif"),
            ("JPEG files", "*.jpg *.jpeg"),
            ("PNG files", "*.png"),
            ("All files", "*.*"),
        ]

        filename = filedialog.askopenfilename(title="Choose Photo", filetypes=filetypes)

        if filename:
            try:
                # Load and display image
                image = cv2.imread(filename)
                if image is not None:
                    self.display_frame(image)
                    self.status_var.set(f"Loaded: {os.path.basename(filename)}")  # noqa: PTH119
                else:
                    messagebox.showerror("Error", "Failed to load image file")

            except Exception as e:
                messagebox.showerror("Error", f"Failed to load image: {e}")

    def start_streaming(self) -> None:
        """Start camera streaming"""
        if not self.camera_manager.current_camera:
            messagebox.showwarning("Warning", "Please select a camera first")
            return

        success = self.camera_manager.start_streaming(self.on_new_frame)
        if success:
            self.streaming_var.set(True)
            self.start_btn.config(state="disabled")
            self.stop_btn.config(state="normal")
            self.status_var.set("Streaming started")
        else:
            messagebox.showerror("Error", "Failed to start streaming")

    def stop_streaming(self) -> None:
        """Stop camera streaming"""
        self.camera_manager.stop_streaming()
        self.streaming_var.set(False)
        self.start_btn.config(state="normal")
        self.stop_btn.config(state="disabled")
        self.status_var.set("Streaming stopped")

    def on_new_frame(self, frame: np.ndarray) -> None:
        """Handle new frame from camera"""
        # Display locally if enabled
        if self.local_preview_var.get():
            self.display_frame(frame)

    def display_frame(self, frame: np.ndarray) -> None:
        """Display a frame in the video canvas"""
        try:
            # Convert BGR to RGB
            if len(frame.shape) == 3:  # noqa: PLR2004, SIM108
                frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
            else:
                frame_rgb = frame

            # Convert to PIL Image
            image = Image.fromarray(frame_rgb)

            # Calculate display size (maintain aspect ratio)
            canvas_width = self.video_canvas.winfo_width()
            canvas_height = self.video_canvas.winfo_height()

            if canvas_width > 1 and canvas_height > 1:  # Canvas is initialized
                img_width, img_height = image.size

                # Calculate scaling factor
                scale_w = canvas_width / img_width
                scale_h = canvas_height / img_height
                scale = min(scale_w, scale_h, 1.0)  # Don't upscale

                new_width = int(img_width * scale)
                new_height = int(img_height * scale)

                # Resize image
                if scale < 1.0:
                    image = image.resize((new_width, new_height), Image.Resampling.LANCZOS)

            # Convert to PhotoImage
            self.photo_image = ImageTk.PhotoImage(image)

            # Clear canvas and display image
            self.video_canvas.delete("all")

            # Center the image
            canvas_center_x = self.video_canvas.winfo_width() // 2
            canvas_center_y = self.video_canvas.winfo_height() // 2

            self.current_image_label = self.video_canvas.create_image(
                canvas_center_x, canvas_center_y, anchor=tk.CENTER, image=self.photo_image
            )

        except Exception as e:
            logger.exception(f"Error displaying frame: {e}")

    def capture_frame(self) -> None:
        """Capture and save the current frame"""
        try:
            filename = self.camera_manager.capture_frame()
            if filename:
                self.status_var.set(f"Frame saved: {filename}")
                messagebox.showinfo("Capture", f"Frame saved as: {filename}")
            else:
                messagebox.showwarning("Warning", "No frame available to capture")

        except Exception as e:
            messagebox.showerror("Error", f"Failed to capture frame: {e}")

    def update_status(self) -> None:
        """Update status information periodically"""
        # Schedule next update
        self.root.after(1000, self.update_status)

    def on_closing(self) -> None:
        """Handle application closing"""
        try:
            # Stop streaming
            self.camera_manager.stop_streaming()

            # Release camera
            self.camera_manager.release()

            # Destroy window
            self.root.destroy()

        except Exception as e:
            logger.exception(f"Error during cleanup: {e}")
            self.root.destroy()

    def run(self) -> None:
        """Start the application"""
        try:
            self.root.mainloop()
        except KeyboardInterrupt:
            self.on_closing()
