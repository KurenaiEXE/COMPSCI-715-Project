# To run this file, the host system needs flask and tensorflow packages installed on the machine
#
#  num_of_classes - defines the number of classes that the model can detect. This needs to be set everytime a new model is loaded.
#  model_path - defines the path to the model to be loaded
from flask import Flask, jsonify
from flask import abort
from flask import make_response
from flask import request

from keras.applications import  vgg16
from keras.models import Sequential
from keras.layers.core import Dense
import numpy as np
import cv2

app = Flask(__name__)

num_of_classes = 9
model_path = "D:\Eclipse Workspace\webService\model\sign_full_load_vgg_2_C9.h5"

#######################################################
# initialize model
#######################################################
model = Sequential()
pre_trained_model = vgg16.VGG16()

i=0
for layer in pre_trained_model.layers:
    i = i + 1
    if i < len(pre_trained_model.layers):
        model.add(layer)

model.add(Dense(num_of_classes, activation='softmax'))
model.load_weights(model_path)
model._make_predict_function()
# model.summary()

@app.errorhandler(404)
def not_found(error):
    return make_response(jsonify({'error': 'Not found'}), 404)

@app.route('/modelSummary', methods=['GET'])
def get_tasks():
    model.summary()
    return "Can't show summary because it's complicated !!"

@app.route('/predict', methods=['POST'])
def create_task():
    if not request.json or not 'image' in request.json:
        abort(400)

    loc = request.json['image']
    alpha_class = request.json['class']
    image = cv2.imread(loc)
    image = cv2.resize(image, (224,224))
    print(image.shape)
    resized_image = np.expand_dims(image, axis=0)
    print(resized_image.shape)

    # predicted_class = model.predict_classes(resized_image)
    predicted_probability = model.predict(resized_image)
    # predicted_class = model.predict_classes(resized_image)
    # print(predicted_class)
    print(predicted_probability[0][alpha_class])
    return str(predicted_probability[0][alpha_class]), 201

if __name__ == '__main__':
    app.run(debug=True)
