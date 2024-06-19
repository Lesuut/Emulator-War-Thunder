using System;
using System.Collections.Generic; // For List
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Emulator_War_Thunder_Server_Forms
{
    public partial class MainForm : Form
    {
        private TcpListener serverSocket;
        private bool isServerRunning;
        private List<TcpClient> connectedClients; // List to keep track of connected clients

        private CustomeVirtualInput virtualImput;

        public static bool inputAccept = true;

        private Color defJsonDebugTextForeColor;

        private PackageFactory packageFactory = new PackageFactory("Emulator", TypeCrew.Server);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        public MainForm()
        {
            InitializeComponent();
            isServerRunning = false;
            connectedClients = new List<TcpClient>(); // Initialize the list of connected clients
            virtualImput = new CustomeVirtualInput(ConsoleLog);
            defJsonDebugTextForeColor = jsonDebugText.ForeColor;
            StartServerAsync();
            StartKeyListenerAsync();
        }

        private async void StartServerAsync()
        {
            try
            {
                serverSocket = new TcpListener(IPAddress.Any, 7000);
                ConsoleLog("Server Started");

                serverSocket.Start();
                isServerRunning = true;

                while (isServerRunning)
                {
                    TcpClient clientSocket = await serverSocket.AcceptTcpClientAsync();
                    lock (connectedClients)
                    {
                        connectedClients.Add(clientSocket); // Add client to the list                    
                    }
                    ConsoleLog("Client connected");

                    _ = HandleClientAsync(clientSocket);
                }
            }
            catch (Exception ex)
            {
                ConsoleLog($"Error: {ex.Message}");
            }
        }

        private async Task HandleClientAsync(TcpClient clientSocket)
        {
            try
            {
                NetworkStream stream = clientSocket.GetStream();
                byte[] bytes = new byte[512];

                while (isServerRunning)
                {
                    int bytesRead = await stream.ReadAsync(bytes, 0, bytes.Length);
                    if (bytesRead == 0)
                        break; // Если нет данных для чтения, прерываем цикл

                    string message = Encoding.ASCII.GetString(bytes, 0, bytesRead);

                    if (IsJson(message))
                    {
                        if (inputAccept)
                        {
                            _ = virtualImput.TryAction(JsonDataSerializer.DeserializeJson<Package>(message), new GameSettingsData(
                                GearsQuantityForwardInput,
                                GearsQuantityBackInput,
                                CurrentGearText,
                                BroadcastMessage,
                                packageFactory,
                                MainGunShotAction));
                            jsonDebugText.ForeColor = defJsonDebugTextForeColor;
                            jsonDebugText.Text = message;
                        }
                    }
                    else
                    {
                        ConsoleLog(message);

                        if (CheckForBrokenPacket(message))
                        {
                            jsonDebugText.ForeColor = Color.Red;
                        }
                        else
                        {
                            jsonDebugText.ForeColor = Color.Cyan;
                        }
                        jsonDebugText.Text = message;
                    }
                }
            }
            catch (IOException ex)
            {
                ConsoleLog($"Connection closed by client: {ex.Message}");
            }
            catch (Exception ex)
            {
                ConsoleLog($"Error: {ex.Message}");
            }
            finally
            {
                lock (connectedClients)
                {
                    connectedClients.Remove(clientSocket); // Remove client from the list
                }
                clientSocket.Close();
            }
        }

        private bool IsJson(string input)
        {
            input = input.Trim();
            if ((input.StartsWith("{") && input.EndsWith("}")) || // For object
                (input.StartsWith("[") && input.EndsWith("]")))   // For array
            {
                try
                {
                    var obj = JToken.Parse(input);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
            }
            return false;
        }

        private bool CheckForBrokenPacket(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return input.Contains("{") || input.Contains("}");
        }

        private void ConsoleLog(string message)
        {
            DateTime now = DateTime.Now;
            string timeString = string.Format("{0:mm:ss.fff}", now);
            string logMessage = $"[{timeString}] {message}";

            consoleBox.Items.Add(logMessage);

            consoleBox.SelectedIndex = consoleBox.Items.Count - 1;
            consoleBox.SelectedIndex = -1;
        }

        private async void StartKeyListenerAsync()
        {
            await Task.Run(() => KeyListenerLoop());
        }

        private void KeyListenerLoop()
        {
            while (true)
            {
                if (IsAltQPressed())
                {
                    inputAccept = !inputAccept;
                    ConsoleLog($"Keyboard control: {inputAccept}");
                    DisableInputSimulationLable.Text = $"Active: {inputAccept}";
                }
                Task.Delay(100).Wait(); // небольшая задержка для снижения нагрузки на процессор
            }
        }

        private bool IsAltQPressed()
        {
            return (GetAsyncKeyState(Keys.Menu) & 0x8000) != 0 && (GetAsyncKeyState(Keys.Q) & 0x8000) != 0;
        }

        private void BroadcastMessage(string message, TcpClient tcpClient)
        {
            jsonDebugText.Text = message;

            byte[] messageBytes = Encoding.ASCII.GetBytes(message);

            try
            {
                NetworkStream stream = tcpClient.GetStream();
                if (stream.CanWrite)
                {
                    stream.WriteAsync(messageBytes, 0, messageBytes.Length);
                }
            }
            catch (Exception ex)
            {
                ConsoleLog($"Failed to send message to a client: {ex.Message}");
            }
        }
        private void BroadcastMessage(string message)
        {
            lock (connectedClients)
            {
                foreach (var client in connectedClients)
                {
                    BroadcastMessage(message, client);
                }
            }
        }
        private void BroadcastMessage(Package package)
        {
            string json = JsonDataSerializer.SerializeToJsonString(package);
            BroadcastMessage(json);
        }
        private void BroadcastMessage(Package package, TcpClient tcpClient)
        {
            string json = JsonDataSerializer.SerializeToJsonString(package);
            BroadcastMessage(json, tcpClient);
        }
        private void SayHiButton_Click(object sender, EventArgs e)
        {
            BroadcastMessage("Hello, everyone from the Server!");
        }

        // Post

        private PackageValueInt GetSuitableGearBox()
        {
            PackageValueInt package = packageFactory.GetPackageValueInt("Suitable Gear Box");

            ConsoleLog("GetSuitableGearBox");

            int currentGear = 0;

            if (GearsQuantityForwardInput != null && int.TryParse(GearsQuantityForwardInput.Text, out int maxGearValue))
            {
                currentGear = maxGearValue;
            }
            else
            {
                currentGear = 4;
            }

            if (currentGear <= 4)
            {
                package.ValueInt = 4;
                return package;
            }
            else if (currentGear > 4 && currentGear <= 6)
            {
                package.ValueInt = 6;
                return package;
            }
            else if (currentGear > 6 && currentGear <= 8)
            {
                package.ValueInt = 8;
                return package;
            }
            else
            {
                package.ValueInt = 10;
                return package;
            }
        }

        private void GearsQuantityForwardInput_TextChanged(object sender, EventArgs e)
        {
            BroadcastMessage(GetSuitableGearBox());
        }
        private async void SetProjectileButton_Click(object sender, EventArgs e)
        {
            await SetProjectiles();
        }
        private async Task SetProjectiles()
        {
            SetReloadTime();
            await Task.Delay(500);
            if (comboBoxProjectile1.SelectedIndex != 0)
            {
                PackageProjectile packageProjectile = packageFactory.GetPackageProjectile("Set Projectile");
                packageProjectile.IdButton = 1;
                packageProjectile.ProjectileId = comboBoxProjectile1.SelectedIndex;
                int count;
                if (Amunition1Count != null && int.TryParse(Amunition1Count.Text, out int parseValue))
                {
                    count = parseValue;
                }
                else
                {
                    count = 1;
                }
                packageProjectile.ProjectileCount = count;
                ConsoleLog($"Set Projectile IdButton:{packageProjectile.IdButton} ProjectileId: {packageProjectile.ProjectileId} ProjectileCount: {packageProjectile.ProjectileCount}");
                BroadcastMessage(packageProjectile);
            }
            await Task.Delay(500);
            if (comboBoxProjectile2.SelectedIndex != 0)
            {
                PackageProjectile packageProjectile = packageFactory.GetPackageProjectile("Set Projectile");
                packageProjectile.IdButton = 2;
                packageProjectile.ProjectileId = comboBoxProjectile2.SelectedIndex;
                int count;
                if (Amunition2Count != null && int.TryParse(Amunition2Count.Text, out int parseValue))
                {
                    count = parseValue;
                }
                else
                {
                    count = 1;
                }
                packageProjectile.ProjectileCount = count;
                ConsoleLog($"Set Projectile IdButton:{packageProjectile.IdButton} ProjectileId: {packageProjectile.ProjectileId} ProjectileCount: {packageProjectile.ProjectileCount}");
                BroadcastMessage(packageProjectile);
            }
        }

        private void SetReloadTime()
        {
            PackageValueFloat package = packageFactory.GetPackageValueFlaot("Set Reload Time");
            float time;
            if (ReloadTimeTextBox != null && float.TryParse(ReloadTimeTextBox.Text, out float parseValue))
            {
                time = parseValue;
            }
            else
            {
                time = 10;
            }
            package.ValueFloat = time;
            BroadcastMessage(package);
            ConsoleLog($"SetReloadTime: {package.ValueFloat}s");
        }

        private void ResetReloadingButton_Click(object sender, EventArgs e)
        {
            Package package = packageFactory.GetPackage("Reset Reloading");
            BroadcastMessage(package);
            virtualImput.ResetGun();
            ConsoleLog("Reset Gun");
        }
        private void MainGunShotAction()
        {
            Package package = packageFactory.GetPackage("Shot Main Gun");
            BroadcastMessage(package);
        }
        private void SetGunCaliber()
        {
            PackageValueInt package = packageFactory.GetPackageValueInt("Set Gun Caliber");
            package.ValueInt = GunCaliber.Value;
            BroadcastMessage(package);
            switch (GunCaliber.Value)
            {
                case 0:
                    ConsoleLog("Set Gun Caliber 50mm");
                    break;
                case 1:
                    ConsoleLog("Set Gun Caliber 105mm");
                    break;
                case 2:
                    ConsoleLog("Set Gun Caliber 155mm");
                    break;
                default:
                    break;
            }
        }

        private void GunCaliber_Scroll(object sender, EventArgs e)
        {
            SetGunCaliber();
        }

        private void resetGearButton_Click(object sender, EventArgs e)
        {
            virtualImput.ResetGear();
        }
    }
}
