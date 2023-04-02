# ACC Server Results Companion

ACC Server Results Companion is an application that imports the session result files created by ACC Server. I can connect remotely via FTP or to a local folder where the files have been downloaded or created by a local instance of ACC Server.

The data from the result files is imported into a local database and presented in various views with the ability to export to CSV or Excel.

The latest release can be found [here](https://github.com/testpossessed/acc-server-results-companion/releases). Simply download and run ACCServerResultsCompanionSetup.exe.
The application will install then launch automatically. Each time the application is started it will automatically update to the latest version before launching.

The application provides guidance on getting started the first time you start it.

PLEASE NOTE:  This application and documentation is in the early stages of development so it will change frequently.

# Release History

## v1.0.0-beta.7 2003-04-02

### Fixed
- Enabled export of all pages when exporting to Excel

## v1.0.0-beta.6 2003-04-01

### Added
- Refresh button on toolbar to reload all data grids

### Fixed
- Issue where driver manager was not refreshing after sync.

## v1.0.0-beta.5 2003-03-31

### Fixed
- Timing issue where entry list files were not always being processed before related session files

## v1.0.0-beta.4 2003-03-31

### Changed
- Added driver category to class mapping override to server editor
- Removed Our Category override throughout
- Updated all grids to show driver class using server mapping

## v1.0.0-beta.3 2003-03-29

### Added
- Driver Manager
- Support for overriding First Name, Last Name and Nationality
- Support for assigning own driver category e.g. PRO, AM

### Changed
- All data grids to user driver overrides

## v1.0.0-beta.2 2003-03-28

### Added
- Capture of driver details from entry lists
- Display of driver category and country in data grids

## v1.0.0-beta.1 2003-03-27

### Added
- Laps tab
- Penalties tab
- Switched to SynFusion WPF controls
- Export to Excel for all tabs

### Changed
- Resolved issues with different types of result files

## v1.0.0-alpha.4 2003-03-25

### Added
- Progress window during server sync process
- Support for FTP folder in Server Details dialog
- Getting started guidance on first run
- Split bar to adjust space taken by left and right panels


## v1.0.0-alpha.3 2003-03-24

### Added
- Display leader board for selected session

## v1.0.0-alpha.2 2003-03-24

### Added
- Display of selected session details

### Fixed
- Error on startup when no servers registered


## v1.0.0-aplha.1 2003-03-25

Initial release

### Added
- Ability to register and save details of server
- Ability to switch between servers
- Import new result files from selected server
