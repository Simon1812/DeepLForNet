
using DeepLForNet.NeuralNetwork.Domains;
using DeepLForNet.NeuralNetwork.Models;
using DeepLForNet.NeuralNetwork.Models.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Artificial.Neural.Network.Number.View {
    public partial class Form1 : Form {
        private readonly FileDomain _fileDomain;
        private readonly NeuralNetworkDomain _neuralNetworkDomain;
        private TraningModel _traningModel;
        private LabelFile _labelFile;
        private PixelFile _pixelFile;
        private int Index = 0;

        public Form1() {
            InitializeComponent();

            _fileDomain = new FileDomain();
            _neuralNetworkDomain = new NeuralNetworkDomain();
        }



        private void Form1_Load(object sender, EventArgs e) {
            _labelFile = _fileDomain.GetLabelFile();
            _pixelFile = _fileDomain.GetPixelFile();

            //var newLabelData = new List<short>();
            //var newPixelData = new List<List<double>>();

            //for (int i = 0; i < _labelFile.ImagesNumbers.Count(); i++) {
            //    if (_labelFile.ImagesNumbers[i] == 0 || _labelFile.ImagesNumbers[i] == 1) {
            //        newLabelData.Add(_labelFile.ImagesNumbers[i]);
            //        newPixelData.Add(_pixelFile.ImageGreyValues[i]);
            //    }
            //}

            //_labelFile.ImagesNumbers = newLabelData;
            //_labelFile.NumberOfItems = newLabelData.Count();

            //_pixelFile.ImageGreyValues = newPixelData;
            //_pixelFile.NumberOfItems = newPixelData.Count();
            DisplayItem();

            for (int i = 0; i < 5; i++) {
                TrainBackprop();
            }
            UpdateModelInfo();
        }

        private void ShowResult_Click(object sender, EventArgs e) {
            _traningModel.Result = _labelFile.ImagesNumbers[Index];
            var result = _neuralNetworkDomain.Calcute(_traningModel, _pixelFile.ImageGreyValues[Index]);
            thatIsLabel.Text = "Der Pc glaubt es ist eine: " + result;
            thatIsLabel.Visible = true;
            reallynumber.Text = "Der tatsächliche Wert ist: " + _labelFile.ImagesNumbers[Index];
            reallynumber.Visible = true;
            errorLabel.Text = "Fehler: " + _traningModel.Network.Error;
            errorLabel.Visible = true;
            UpdateModelInfo();
        }

        private void TrainBackprop() {
            _traningModel.Network.Success.Clear();
            _traningModel.Network.Errors.Clear();
            var index = 0;
            for (int i = 0; i < 400; i++) {
                for (int s = 0; s < 50; s++) {
                    _traningModel.Result = _labelFile.ImagesNumbers[index];
                    _neuralNetworkDomain.Calcute(_traningModel, _pixelFile.ImageGreyValues[index]);

                    //if ((double)_traningModel.Network.Error > 0.0005) {
                    _neuralNetworkDomain.Backpropagtion(_traningModel);
                    //}

                    index++;
                }
                _neuralNetworkDomain.UpdateNetwork(_traningModel);
            }
            _traningModel.Network.Mutation += 1;
        }


        private void Next_Click(object sender, EventArgs e) {
            Index++;
            thatIsLabel.Visible = false;
            reallynumber.Visible = false;
            errorLabel.Visible = false;
            DisplayItem();
        }

        private void DisplayItem() {
            var columns = _pixelFile.Columns;
            var rows = _pixelFile.Rows;

            var greyValues = _pixelFile.ImageGreyValues[Index];
            if (_traningModel == null) {
                _traningModel = _neuralNetworkDomain.GetTraningModel(columns * rows, new Dictionary<int, Dictionary<Activationfunctions, int>> {
                    { 1, new Dictionary<Activationfunctions, int> { { Activationfunctions.TanH, 40 } } },
                    { 2, new Dictionary<Activationfunctions, int> { { Activationfunctions.TanH, 20 } } },
                    //{ 3, new Dictionary<Activationfunctions, int> { { Activationfunctions.Sigmoid, 15 }, { Activationfunctions.TanH, 10 } } },
                },
                //_traningModel = _neuralNetworkDomain.GetTraningModel(columns * rows, new Dictionary<int, Dictionary<Activationfunctions, int>> {
                //    { 1, new Dictionary<Activationfunctions, int> { { Activationfunctions.Softplus, 10 }, } },
                //    { 2, new Dictionary<Activationfunctions, int> { { Activationfunctions.Softplus, 10 }, } },
                //    //{ 3, new Dictionary<Activationfunctions, int> { { Activationfunctions.ReLU, 15 }, } },
                //},
                OutputFunctions.Sigmoid, new List<string> {
                    "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
                });

            } else {
                _traningModel.Result = _labelFile.ImagesNumbers[Index];
            }


            numberPictureBox.Image = new Bitmap(columns, rows);

            var index = 0;
            for (int y = 0; y < columns; y++) {
                for (int x = 0; x < rows; x++) {
                    int greyValue = 255 - ((int)(greyValues[index] * 255));
                    var color = Color.FromArgb(greyValue, greyValue, greyValue);
                    (numberPictureBox.Image as Bitmap).SetPixel(x, y, color);
                    index++;
                }
            }

            numberPictureBox.Image = new Bitmap((numberPictureBox.Image as Bitmap), new Size(columns * 6, rows * 6));


            numberPictureBox.Refresh();
        }

        private void UpdateModelInfo() {
            accuracyLabel.Text = "Genauigkeit: " + _traningModel.Network.RateOfSucces;
            accuracyLabel.Visible = true;
            generationLabel.Text = "Generation:" + _traningModel.Network.Mutation;
            generationLabel.Visible = true;
        }

        private void trainingButton_Click(object sender, EventArgs e) {
            TrainBackprop();
            UpdateModelInfo();
        }

        private void saveButton_Click(object sender, EventArgs e) {
            var json = _neuralNetworkDomain.Save(_traningModel);
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var filename = "neuralNet_" + Math.Round(_traningModel.Network.RateOfSucces, 4) + "_" + _traningModel.Network.Mutation + "_" + Guid.NewGuid();
            File.WriteAllText($"{path}\\..\\..\\..\\..\\Models\\{filename}.json", json);
        }

        private void loadButton_Click(object sender, EventArgs e) {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                openFileDialog.InitialDirectory = path.Replace(@"Artificial.Neural.Network.Number.View\bin\Debug\netcoreapp3.1", "Models");
                openFileDialog.Filter = "Json files (*.json)|*.json"; // "txt files (*.json)|*.txt|All files (*.*)|*.*";
                
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream)) {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            var traningsModel = _neuralNetworkDomain.Load(fileContent);
            _traningModel = traningsModel;
            UpdateModelInfo();
        }
    }
}
