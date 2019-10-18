# README
The Unity project for the Jenga VR is in JengaVR folder

## Setting up hand gesture pipeline system
1. Install Python 3.6 (can be downloaded online)
2. Open Command Prompt
3. Run "install pip" for keras, flask, tensorflow
..* pip must be installed in the PC to run these commands. Download get-pip.py and run it to install
4. Change directory to the folder containing the cnnServer.py file
..* if new model is created, the path in cnnServer.py can be changed to the new model file
5. Run command "set FLASK_APP=cnnServer.py"
6. Run command "flask run"
