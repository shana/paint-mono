# Basics #

Paint.NET has a very clean isolation between the engine and the system specific components.

The ideal scenario is to ship a SystemLayer.dll file that will replace the one that comes with the official distribution of Paint.NET.    Although I had originally hoped that the code could continue to support Mono and .NET on the same codebase, Rick suggested that I avoid this and go directly the route of replacing the library.

Ideally the port would only require the src/SystemLayer directory to be ported but currently Paint.NET still contains a few places in the core engine that must be modified (for example, at startup the program checks the list of libraries and resources to be in the target directory, but some of these assemblies are not necessary for Linux or are useless on Linux) this has required some changes in the core of Paint.NET.

In places that must be revisited in the future (unfinished implementation, stubs, routines turned into no-ops) I have added a WriteLine statement so we can keep track of what is missing.   A complete port should produce no output on the console.

## Directions ##

The port is good enough to start the application, but many features are still not tested and there are many P/Invokes that remain in the source code.   I have not really spent much time trying to test it, but testing will exhibit many features that must be ported to Linux.

What needs to be done:
  * Test the application.
  * File issues for every issue found.
    * If it is a Paint.Net issue, file in http://code.google.com/p/paint-mono/issues/list
    * If it is a Mono issue, file in http://www.mono-project.com/Bugs
  * Provide fixes in either Paint.NET or Mono's Windows.Form implementation.

Repeat until we are done, and the application is working like a charm.

## Current State ##

Paint.NET starts up, it is possible to load and save files, and run many of the effects.   There are still a few bugs left and the SystemLayer work has not been completed, so a few things might break every once in a while.

It is recommended that you start with Mono 1.2.6, although a few features (saving) requires Mono from SVN as there were a few bugs that prevented it from running correctly.

## P/Invoke ##

There are a number of cases where Paint.NET is using P/Invoke, in some cases it is using functionality from the Windows OS that is available in Unix in a different form.  Porting this code should be pretty straight forward.

In some other cases the P/Invokes are used to call into features in the various Windows.Forms controls that is not exposed in the Windows.Forms API but is part of the underlying Win32 control.   In these cases, sometimes the functionality is also part of the internals of Mono's Managed Windows.Forms implementation.   In those cases you can access the internal features by using Reflection and Invoke, for example, consider this code that replaces the call to P/Invoke with an internal call

```
	MethodInfo drop_down = typeof (ComboBox).GetMethod ("DropDownListBox",
                BindingFlags.Instance|BindingFlags.NonPublic);
	if (drop_down == null){
		Console.WriteLine ("ShowComboBox: Warning, no DropDownlistBox found");
		return;
	}
	drop_down.Invoke (comboBox, new object [0]);
```

This was just an illustration, a better way is to just call the method that takes care of this on the comboBox:
```
        comboBox.Dropped = value;
```


# Building Paint.NET on Linux #

First, you should check the code out from SVN, once you have the source code, the easiest way of testing it is to use the standard Unix source configuration and installation setup:

cd src
./configure
make
make install

Alternatively, you can use MonoDevelop to build Paint.NET.   To use it, just open the src/paintdotnet.sln solution and rebuild the solution (press F5) to run.

# Original Source #

To track the changes and assist us in writing a 'Lessons in Porting' document, the original source code as obtained from http://www.getpaint.net is checked into this directory:

http://paint-mono.googlecode.com/svn/vendor/pdn_src_3_0

This is the vendor/pdn\_src\_3\_0 branch.

# Contributing #

If you want to contribute to this effort, please mail me your login information, and I will add you to this project.

Alternatively, you can contact other members of the administration group.