# ACC Server Results Companion

ACC Server Results Companion is an application that imports the session result files created by ACC Server. I can connect remotely via FTP or to a local folder where the files have been downloaded or created by a local instance of ACC Server.

The data from the result files is imported into a local database and presented in various views with the ability to export to CSV or Excel.

The latest release can be found [here](https://github.com/testpossessed/acc-server-results-companion/releases). Simply download and run ACCServerResultsCompanionSetup.exe.
The application will install then launch automatically. Each time the application is started it will automatically update to the latest version before launching.

The application provides guidance on getting started the first time you start it.

PLEASE NOTE:  This application and documentation is in the early stages of development so it will change frequently.

# Release History

## v1.3.4 2023-05-03

### Added
- Penalty Ratio to Driver Performance report

## v1.3.3 2023-05-03

### Fixed
- Issue where penalty reporting was applying pentalies to all drivers of a session

### Added
- Penalty Count and Penalty Value Total columns to Driver Performance Report

## v1.3.2 2023-05-03

### Added
- Penalty Count and Penalty Value Total columns to Driver Performance Report

## v1.3.1 2023-04-26

### Added
- Added count of races to Driver Performance report

### Changed
- Moved reports to tabs rather than buttons

## v1.3.0 2023-04-22

### Added
- Meta data for new 2023 content

## v1.2.2 2023-04-18

### Added
- Driver race performance report

## v1.2.1 2023-04-10

### Changed
- Add Excel like filtering to all data grids

## v1.2.0 2023-04-08

### Added
- Filtering to Fastest Laps reporting tool

### Changed
- Added better handling of failed FTP operations to avoid crashing the app
- Improved layout of Server Stats view for smaller screens

## v1.1.1 2023-04-07

### Fixed
- Implementation of folder based server import

### Changed
- Reduced size of splash screen

## v1.1.0 2023-04-07

### Added
- Server stats tab

### Fixed
- Issue where drivers were not being updated on import of entry list

## v1.0.0 2023-04-05

### Added
- Report tool to provide overall fastest laps with option to filter by server
- Released as v1 as primary function of importing and providing raw data is done and tested

## v1.0.0-beta.8 2023-04-03

### Added
- Tool to load SimGrid standings export from JSON to display in grids an export to Excel

## v1.0.0-beta.7 2023-04-02

### Fixed
- Enabled export of all pages when exporting to Excel

## v1.0.0-beta.6 2023-04-01

### Added
- Refresh button on toolbar to reload all data grids

### Fixed
- Issue where driver manager was not refreshing after sync.

## v1.0.0-beta.5 2023-03-31

### Fixed
- Timing issue where entry list files were not always being processed before related session files

## v1.0.0-beta.4 2023-03-31

### Changed
- Added driver category to class mapping override to server editor
- Removed Our Category override throughout
- Updated all grids to show driver class using server mapping

## v1.0.0-beta.3 2023-03-29

### Added
- Driver Manager
- Support for overriding First Name, Last Name and Nationality
- Support for assigning own driver category e.g. PRO, AM

### Changed
- All data grids to user driver overrides

## v1.0.0-beta.2 2023-03-28

### Added
- Capture of driver details from entry lists
- Display of driver category and country in data grids

## v1.0.0-beta.1 2023-03-27

### Added
- Laps tab
- Penalties tab
- Switched to SynFusion WPF controls
- Export to Excel for all tabs

### Changed
- Resolved issues with different types of result files

## v1.0.0-alpha.4 2023-03-25

### Added
- Progress window during server sync process
- Support for FTP folder in Server Details dialog
- Getting started guidance on first run
- Split bar to adjust space taken by left and right panels


## v1.0.0-alpha.3 2023-03-24

### Added
- Display leader board for selected session

## v1.0.0-alpha.2 2023-03-24

### Added
- Display of selected session details

### Fixed
- Error on startup when no servers registered


## v1.0.0-aplha.1 2023-03-25

Initial release

### Added
- Ability to register and save details of server
- Ability to switch between servers
- Import new result files from selected server
