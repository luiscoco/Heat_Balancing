The *.MIX files are ASCII files that can be read with any editor such as NOTEPAD.

The first line in these files contains the name of the mixture (and for the
refrigerants, the fluids in the mixture and their compositions in the form used
by ASHRAE, but this is only information that is displayed while choosing a
predefined mixture).

The second line contains the values for m/(g/mol), Tc/K, Pc/kPa, and
Dc/(mol/dm^3) (molar mass and critical point values for temperature, pressure,
and density).  These four numbers are used by the program as information only
when the Info button is pressed in the GUI while selecting a predefined mixture,
when calling the REFPROP routine to obtain the critical point values, or, most
importantly, when the program needs the critical point when a predefined mixture
is in use.  When a predefined mixture is saved under the Substance/Specify
Composition menu, these numbers will be calculated with the mixture models and
written to the file.  ***It is important that these numbers are correct or the
program may not function correctly.***

The third line contains the number of components in the mixture.  The lines
below that list the fluid file names that are used in the mixture (with the .FLD
extension included).  Below that are the mole fractions of each component in the
same order as the fluid files.  The values must sum to one.

The bottom line is always zero and is available for use as a flag by the program
in future applications.
