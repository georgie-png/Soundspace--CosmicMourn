# Soundspace
 
### These are the basic tools and prefabs I created to produce the AR SoundSpace App/ Cosmic mourn experience.

Check Out The Game Play Video Here : <a href="https://www.youtube.com/watch?v=bRlox-qyZBc&t=4s" target="_blank">VIDEO</a>

Or Try It On your Device Here : 

 - [ios](https://apps.apple.com/app/id1529811825)
 - [Andriod](https://play.google.com/store/apps/details?id=com.Lorn.SoundSpace.Four&hl=en_GB)


### To Get It Working In Unity!!!

All you need to do is create a new unity project, I used Universal render pipeline on version 2019.4.7f1. 

 - Go to assets > importPackage > custom package 

 - Import the the packages from 'Soundspace Assets' you downloaded from this repository.

 - Open Scenes/Cosmic Mourn Main Scene. 

 - Build to an AR ready device.



### Inside The Assets Folder:

 - Planet Controller - Holding prefabs for the Planets as well as the scripts, materials and shaders. The main prefab is 'Base Planet'.

 - Particle Systems - Holding prefabs of a few types of sound reactive particle systems as well as their scripts, materials, and shader.

 - Music Samples - Holding all the recordings I recorded and used for the piece.

 - Other bits - Mainly to help render in the Universal Render Pipeline, including postprocessing. 



### Including Project Settings

I included this due to their being a slight compatibility problem with AR Packages, it should load the correct set. If it doesn't work bring in AR packages released on roughly the same date.
