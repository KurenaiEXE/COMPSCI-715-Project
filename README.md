# JengASL: Sign Language Learning using VR Jenga Game
Project repo for the JengASL software.
The `GestureRecognition` directory holds all python scripts for ML part, and the `JengaVR` directory is the main unity project.

## Project Directory
```
 COMPSCI-715-Project
  |- .vs : Visual Studio specific files
  |   |- Jenga VR
  |       |- (miscs)
  |
  |- Hand Models Blender
  |   |- Rigged_Hand_blend : textures and miscs
  |   |- alphabet : holds blender files, obj files of ASL alphabet hand gesture models
  |
  |- GestureRecognition
  |   |- cnnServer.py : A simple web server python script
  |   |- imageDetector.ipynb : A python script in jupyter-notebook format for ML model training and testing
  |
  |- JengaVR : A main unity project directory
  |- __pycache__ : (miscs)
  ```

## Setting up hand gesture pipeline system
1. Install Python 3.6 (can be downloaded online)
2. Open Command Prompt
3. Run "install pip" for keras, flask, tensorflow. pip must be installed in the PC to run these commands. Download get-pip.py and run it to install
4. Change directory to the folder containing the cnnServer.py file. If new model is created, the path in cnnServer.py can be changed to the new model file
5. Run command "set FLASK_APP=cnnServer.py"
6. Run command "flask run"
