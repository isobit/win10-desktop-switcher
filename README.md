# Windows 10 Virtual Desktop Switcher

Uses the hidden `IVirtualDesktopManagerInternal` interface to switch desktops directly.

## Usage
`DesktopSwitcher.exe <n>`, where n is the id of the desktop you want to switch to (zero-indexed).

## AutoHotkey
Here is an example AutoHotkey script which binds Alt+1 through Alt+4 to direct desktop switching:

```
!1::Run, DesktopSwitcher.exe 0, , hide
!2::Run, DesktopSwitcher.exe 1, , hide
!3::Run, DesktopSwitcher.exe 2, , hide
!4::Run, DesktopSwitcher.exe 3, , hide
```

## Installation
Just grab the latest release [here](https://github.com/joshglendenning/win10-desktop-switcher/releases/latest).
