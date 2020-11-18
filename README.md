# KingstUIAutomation
Simple multi-purpose UI automation software for Windows. Initially developed to enable unattended operation for KingstVIS logic analyzer software.

Since Kingst doesn't offer any API to control acquisition (and KingstVIS is a Qt5 application, so no hWnd-s for "controls"), the only way to automate the process is to simulate mouse input. Due to the nature of input simulation, this tool can be used for any sort of UI automation, not only KingstVIS. Therefore I've chosen to implement a simple UI-automation script engine that is able to execute XML-serialized (and therefore user-editable) SimulatorScenario classes (examples included under Help menu).

**Done:**

- Mouse movement/clicking, key pressing.
- Point database engine. ReferencedPoint class is already implemented to allow points that are referenced from different corners of non-maximized windows (usefull for controls that are not top-left aligned). The database is XML-serialized (database.xml inside the working directory) and can be edited using a simple text editor like NotePad++ or even Windows Notepad (CR+LF convention is used).
- Screen point recorder: a semi-transparent borderless window acting as an overlay that is designed to facilitate gathering a database of screen coordinates. You can switch between point reference mode (TopLeft, TopRight etc) in a ring manner by pressing left Control.
- Color reader for screen point database (outputs separate ARGB values and consolidated ARGB integers as used in serialization for all points in current database).
- Waiting for pixels to change color (can be used, for example, to wait until KingstVIS catches a trigger, and thus completes an aquisition, to automatically start a new one).
- UI automation scripts that can be edited via XML-serialization (scenario.xml inside the working directory). For example, one can specify the following sequence of actions to be executed: wait for 500mS, then click point called "X" (in the database), then wait for 100mS, then press a key, then wait for a pixel called "Y" to change its color to some ARGB value, then click or press something. You can loop through the script.
- Script execution can be controlled from another application using named pipes (example project attached). The application can be configured to start the pipe listener and hide the window on its startup for unattended operation. Also you can press right Control to break out of a script being executed. (However, after you press it one last iteration may still execute! Have not found the cause of this yet.)
- Window search. I.e. the script won't be executed (will return corresponding non-zero exit code immediately) if a window with specified title can't be found. Window title is specified using title.txt file inside the working directory.
- Some basic GUI.

**Todo:**  Support non-maximized windows (a part of corresponding functionality is already in-place), add the ability to switch window oreder (Win32 API, since Vista it's a rather long way to go). Some GUI improvements. Probably add mouse wheel simulation and right/middle mouse buttons.

**P.S.**

Uses .NET 4.0 (XP-compatible).

InputSimulatorPlus NuGet package is used for clicking and key pressing. However, it fails to move the mouse in the right way: somehow its pixel sizes always correspond to 800x600 effective resolution. Had to manually import WinAPI, SetCursorPos does the job perfectly.

NamedPipeWrapper NuGet package is used to simplify the implementation of named pipes.
