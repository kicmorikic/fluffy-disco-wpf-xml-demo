# fluffy-disco-wpf-xml-demo

WPF app that stores some data in an xml datastore in a users profile.

## Building

Open solution in visual studio 2022 and build it


## Instructions given

Implement a desktop application (either using WinForms or WPF) which displays a table of people containing the following information:

- First Name,
- Last Name
- Street Name
- House Number
- Apartment Number (optional)
- Postal Code
- Town
- Phone Number
- Date of Birth
- Age (read-only)

The application should allow a user to edit the data inline, add new users and delete existing ones. The data should be stored in the XML
file. The file should be located in an appropriate location in the current user profile. Below the table there should be two buttons: "Save" and "Cancel".  When the Save button is pressed changes made by a user should be stored in the XML file. Pressing the "Cancel" button discards user changes and causes the table to be refreshed based on the data stored in the XML file. The buttons should be active only if the table contains unsaved data. After the first startup of the application the table should be empty.

