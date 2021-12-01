# GenTrackDemo_GDR
This project is a Gentech code test to a problem statement not presented herein.
Based on time constraints and several pressing challenges and priority breakdowns I found that even with an adequate timeline, large chunks of time can be consumed by equally pressing matters.
 
A console application in the solution is the main access to features and functions presented herein.
The thought rationale is that if we are to run and evaluate incoming XML files it may be that this would be a timed-repeat process and a console can be run from a scheduled task to run in the context of a service account that has access to specific IO rights(Folder) and then deliver the output files to a destination folder in the processed form.

The Source and destination folders for the project are not provided in the config, but in a production environment would be quintessential to success as practical processing parameters.

Incoming XML files (or the test case "testfile.xml") was parsed by the XSD tool from Visual Studio developer console, that output was then parsed to create the output XMLClass.cs, this is not implemented in the project in the raw form, I changed all arrays to Lists and also made the two root objects "object[]" strongly typed and declared the type instead, allowing object oriented identification as apposed to array positional indexes, this was a personal preference and amounts to less typing and back and forth referencing to make sure I have the correct array index/object etc. 

Two main classes manage the file process, DataHelper validates files existence and serialises the xml data to the XMLListClass, extracted CSV data gets passed to the DataParser class than inspects csv data, validates and builds output files. 

Unit test exist although not in true TDD form, the unit tests were written in haste on the back-end of a mostly working solution (my bad!), not all unit tests have been completed and 1 fails, perhaps if more time were vailable this would have been done before hand, although I have seen legacy applications in maintenance only mode receive unit tests around difficult to debug automated process code so TDD but not in order.

Several more features could be implemented if more was know around a broader requirements gathering, a couple of question answered, i.e. if part of a file is bad (missing 300's) must the whole file fail or just the bad part, who do we inform of failures, log file/event handler or email or all of the above, although I would leave event handler for application failure not process failures. Last question for now is what if the serialisation fails or we have an empty csv section of the file, how do we respond to these failures.

Fin.
