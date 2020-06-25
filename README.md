# KingstUIAutomation
UI automation for KingstVIS logic analyzer software.

Since Kingst doesn't offer any API to control acquisition (and KingstVIS is a Qt5 application, so no hWnd-s for "controls"), the only way to automate the process is to simulate mouse input. Due to the nature of input simulation, this tool can be used for any sort of UI automation, not only KingstVIS.

**Done:** mouse movement/clicking. Screen point recorder and database engine (for maximized windows only!). Scenarios (they can be edited via serialization). Waiting for pixels to change color + pressing individual keys (not tested). Some GUI.

**Todo:** Further testing, some simple calculations to account for non-maximized windows, GUI improvements.

**P.S.** InputSimulatorPlus NuGet package is used for clicking and key pressing. However, it fails to move the mouse in the right way: somehow its pixel sizes always correspond to 800x600 effective resolution. Had to manually import WinAPI, SetCursorPos does the job perfectly.
