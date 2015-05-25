using System;
using Symbol.Barcode2;
using System.Windows.Forms;

namespace FriosillaBarCode
{
    internal class Scanner
    {
        static private Device[] _ourAvailableDevices;
        private uint _scanTimeout;

        /// <summary>
        /// Initialize the Barcode2 object.
        /// </summary>
        public bool InitBarcode()
        {
            // If the Barcode2 object is already initialized then fail the initialization.
            if (Barcode2 != null)
            {
                return false;
            }
            try
            {
                // Get the device selected by the user.
                var myDevice =Select("Barcode",Devices.SupportedDevices);

                if (myDevice == null)
                {
                    //MessageBox.Show(Util.GetResourceString("NoDeviceSelected"), Util.GetResourceString("SelectDevice"));
                    return false;
                }

                // Create the reader, based on selected device.
                Barcode2 = new Barcode2(myDevice);

                // In this sample, we are setting the aim type to trigger. 
                switch (Barcode2.Config.Reader.ReaderType)
                {
                    case READER_TYPES.READER_TYPE_IMAGER:
                        Barcode2.Config.Reader.ReaderSpecific.ImagerSpecific.AimType = AIM_TYPE.AIM_TYPE_TRIGGER;
                        break;
                    case READER_TYPES.READER_TYPE_LASER:
                        Barcode2.Config.Reader.ReaderSpecific.LaserSpecific.AimType = AIM_TYPE.AIM_TYPE_TRIGGER;
                        break;
                }

                Barcode2.Config.Reader.Set();

                // Configuración
                ConfigurarEscaner();
            }

            catch (OperationFailureException ex)
            {
                MessageBox.Show(Util.GetResourceString("InitBarcode") + "\n" +
                                Util.GetResourceString("OperationFailure") + "\n" + ex.Message +
                                "\n" +
                                Util.GetResourceString("Result") + " = " + (Results)((uint)ex.Result)
                    );

                return false;
            }
            catch (InvalidRequestException ex)
            {
                MessageBox.Show(Util.GetResourceString("InitBarcode") + "\n" +
                                Util.GetResourceString("InvalidRequest") + "\n" +
                                ex.Message);

                return false;
            }
            catch (InvalidIndexerException ex)
            {
                MessageBox.Show(Util.GetResourceString("InitBarcode") + "\n" +
                                Util.GetResourceString("InvalidIndexer") + "\n" +
                                ex.Message);

                return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void ConfigurarEscaner()
        {
            // tipos de barcodes
            if (Util.GetSetting("ScanOnlyDataMatrix").Equals("true"))
            {
                Barcode2.Config.Decoders.DisableAll();
                Barcode2.Config.Decoders.DATAMATRIX.Enabled = true;
                Barcode2.Config.Decoders.Set();
            }
            else
            {
                Barcode2.Config.Decoders.RestoreDefaults();
                Barcode2.Config.Decoders.Set();
            }

            // Scan default timeout
            _scanTimeout = Util.GetUIntSetting("ScanTimeout") > 0 ? Util.GetUIntSetting("ScanTimeout") : 5000;

            // tamaño
            var scanDataSize = Util.GetUIntSetting("ScanDataSize") > 0 ? Util.GetUIntSetting("ScanDataSize") : (uint) (ReaderDataLengths.MaximumLabel);
            Barcode2.Config.ScanDataSize = scanDataSize;

            // sonido
            if (Util.GetSetting("SoundEnabled").Equals("false"))
            {
                Barcode2.Config.Scanner.StartBeepTime = 0;
                Barcode2.Config.Scanner.ActivityBeepTime = 0;
                Barcode2.Config.Scanner.FatalBeepTime = 0;
                Barcode2.Config.Scanner.NonfatalBeepTime = 0;
            }

            Barcode2.Config.Scanner.DecodeBeepTime = 0;
            Barcode2.Config.Scanner.Set();
        }

        public static Device Select(string title, Device[] availableDevices)
        {
            _ourAvailableDevices = availableDevices;

            if (_ourAvailableDevices.Length == 0)
            {
                return null;
            }

            if (_ourAvailableDevices.Length == 1)
            {
                return _ourAvailableDevices[0];
            }

            var nSelection = Util.GetUIntSetting("BarcodeDevice");
            return _ourAvailableDevices[nSelection];
        }

        /// <summary>
        /// Stop reading and disable/close the Barcode2 object.
        /// </summary>
        public void TermBarcode()
        {
            // If we have a reader
            if (Barcode2 == null) return;
            try
            {
                // stop all the notifications.
                StopScan();

                // Free it up.
                Barcode2.Dispose();

                // Make the reference null.
                Barcode2 = null;
            }

            catch (OperationFailureException ex)
            {
                MessageBox.Show(Util.GetResourceString("TermBarcode") + "\n" +
                                Util.GetResourceString("OperationFailure") + "\n" + ex.Message +
                                "\n" +
                                Util.GetResourceString("Result") + " = " + (Results)((uint)ex.Result)
                    );
            }
            catch (InvalidRequestException ex)
            {
                MessageBox.Show(Util.GetResourceString("TermBarcode") + "\n" +
                                Util.GetResourceString("InvalidRequest") + "\n" +
                                ex.Message);
            }
            catch (InvalidIndexerException ex)
            {
                MessageBox.Show(Util.GetResourceString("TermBarcode") + "\n" +
                                Util.GetResourceString("InvalidIndexer") + "\n" +
                                ex.Message);
            }
        }

        /// <summary>
        /// Start a scan.
        /// </summary>
        public void StartScan(bool triggerSoftAlways)
        {
            if (Barcode2 == null) return;
            try
            {
                Barcode2.Config.TriggerMode = triggerSoftAlways ? TRIGGERMODES.SOFT_ALWAYS : TRIGGERMODES.HARD;

                // By default, the assemly Symbol.Barcode2 will decode the barcodes whose data length is less than or equal to 55.
                // The user will have to increase ScanDataSize in order to get any longer lengths decoded.
                //myBarcode2.Config.ScanDataSize = (int)(Symbol.Barcode2.ReaderDataLengths.MaximumLabel);
                // Submit a scan.
                Barcode2.Scan(_scanTimeout);
            }

            catch (Symbol.Exceptions.OperationFailureException ex)
            {
                MessageBox.Show(Util.GetResourceString("StartScan") + "\n" +
                                Util.GetResourceString("OperationFailure") + "\n" + ex.Message +
                                "\n" +
                                Util.GetResourceString("Result") + " = " + (Symbol.Results)((uint)ex.Result));
            }
            catch (Symbol.Exceptions.InvalidRequestException ex)
            {
                MessageBox.Show(Util.GetResourceString("StartScan") + "\n" +
                                Util.GetResourceString("InvalidRequest") + "\n" +
                                ex.Message);

            }
            catch (Symbol.Exceptions.InvalidIndexerException ex)
            {
                MessageBox.Show(Util.GetResourceString("StartScan") + "\n" +
                                Util.GetResourceString("InvalidIndexer") + "\n" +
                                ex.Message);

            }
        }

        /// <summary>
        /// Stop all reads on the reader.
        /// </summary>
        public void StopScan()
        {
            //If we have a reader
            if (Barcode2 == null) return;
            try
            {
                // Flush (Cancel all pending reads).
                Barcode2.ScanCancel();
            }

            catch (OperationFailureException ex)
            {
                MessageBox.Show(Util.GetResourceString("StopScan") + "\n" +
                                Util.GetResourceString("OperationFailure") + "\n" + ex.Message +
                                "\n" +
                                Util.GetResourceString("Result") + " = " + (Results)((uint)ex.Result)
                    );
            }
            catch (InvalidRequestException ex)
            {
                MessageBox.Show(Util.GetResourceString("StopScan") + "\n" +
                                Util.GetResourceString("InvalidRequest") + "\n" +
                                ex.Message);
            }
            catch (InvalidIndexerException ex)
            {
                MessageBox.Show(Util.GetResourceString("StopScan") + "\n" +
                                Util.GetResourceString("InvalidIndexer") + "\n" +
                                ex.Message);
            }
        }

        /// <summary>
        /// Provides the access to the Symbol.Barcode.Reader reference.
        /// The user can use this reference for his additional Reader - related operations.
        /// </summary>
        public Barcode2 Barcode2 { get; private set; }

        /// <summary>
        /// Attach a ScanNotify handler.
        /// </summary>
        public void AttachScanNotify(Barcode2.OnScanHandler scanNotifyHandler)
        {
            // If we have a reader
            if (Barcode2 != null)
            {
                // Attach the read notification handler.
                Barcode2.OnScan += scanNotifyHandler;
            }

        }

        /// <summary>
        /// Detach the ScanNotify handler.
        /// </summary>
        public void DetachScanNotify(Barcode2.OnScanHandler scanNotifyHandler)
        {
            if (Barcode2 != null)
            {
                // Detach the read notification handler.
                Barcode2.OnScan -= scanNotifyHandler;
            }
        }

        /// <summary>
        /// Attach a StatusNotify handler.
        /// </summary>
        public void AttachStatusNotify(Barcode2.OnStatusHandler statusNotifyHandler)
        {
            // If we have a reader
            if (Barcode2 != null)
            {
                // Attach status notification handler.
                Barcode2.OnStatus += statusNotifyHandler;
            }
        }

        /// <summary>
        /// Detach a StatusNotify handler.
        /// </summary>
        public void DetachStatusNotify(Barcode2.OnStatusHandler statusNotifyHandler)
        {
            // If we have a reader registered for receiving the status notifications
            if (Barcode2 != null)
            {
                // Detach the status notification handler.
                Barcode2.OnStatus -= statusNotifyHandler;
            }
        }

    }
}
