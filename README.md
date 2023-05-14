# Soundboard

This was my second project using c#. The goal was to make a soundboard that I and my friends could use to play clips in voice chats.

I was not able to get this done to the level I wanted because of my limited experience in developing windows applications. I primarily hit 2 ultimatily unsolvable problems: mixing the audio output of the application with the microphone of the user, and global windows shortcuts.

The first problem required me to write a virtual audio device driver. This was way out of the scope of this project; I did attempt to go around the issue by using VB-Cable but I wasn't able to get it to work and this would not be easy for the average end user to install.

The second problem I faced was making global windows shortcuts. I wanted the soundboard to work if a set string of keys was hit no matter if the application was focused or not. This meant registering windows level shortcuts to the end users pc but they can only be removed before the application is closed. Meaning too many things can go wrong, and messing with an end users global windows shortcuts is just not a good idea.

Other than that all other functionality works: playing clips, playing/pasuing, audio levels, and saving (so that the user doesn't have to put the clips in every time they restart the application). I would like to revisit this in the future after gaining a lot more experience.

The application no longer works (on windows 11 atleast) so I can't provide a picture of what it looks like but the source files are all here.
