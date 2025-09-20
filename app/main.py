#!/usr/bin/env python3
"""
Camera Stream Application - Main Entry Point
"""

import sys

from loguru import logger

from app.gui.main_window import CameraApp


def main() -> int:
    """Main application entry point"""
    try:
        app = CameraApp()
        app.run()
    except KeyboardInterrupt:
        logger.info("\nApplication interrupted by user")
    except Exception as e:
        logger.exception(f"Application error: {e}")
        return 1
    return 0


if __name__ == "__main__":
    sys.exit(main())
