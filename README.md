# ACC Server Results Companion

ACC Server Results Companion is an application that imports the session result files created by ACC Server. I can connect remotely via FTP or to a local folder where the files have been downloaded or created by a local instance of ACC Server.

The data from the result files is imported into a local database and presented in various views with the ability to export to CSV or Excel.

The latest release can be found [here](https://github.com/testpossessed/acc-server-results-companion/releases). Simply download and run ACCServerResultsCompanionSetup.exe.
The application will install then launch automatically. Each time the application is started it will automatically update to the latest version before launching.

The application provides guidance on getting started the first time you start it.

PLEASE NOTE:  This application and documentation is in the early stages of development so it will change frequently.

# Release History

## v1.0.0-alpha.5 2003-03-27

### Added
- Laps tab
- Penalties tab
- Switched to SynFusion WPF controls

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
