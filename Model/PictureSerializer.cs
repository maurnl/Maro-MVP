﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Model
{
    public static class PictureSerializer
    {
        public static Bitmap TurnStringToImage(string bitmampString)
        {
            Bitmap bitmap; // DECLARE A BITMAP

            if (bitmampString != "" && bitmampString != null)
            {
                byte[] bytes = Convert.FromBase64String(bitmampString); // TURN STRING TO BYTES


                using (var ms = new MemoryStream(bytes))
                {
                    bitmap = new Bitmap(Bitmap.FromStream(ms)); // TURN BYTES INTO BITMAP
                }
            }
            else
            {
                bitmap = null;
            }

            return bitmap;
        }

        // * * * * * * * * * *

        public static string UploadImageAsString()
        {
            OpenFileDialog loadPicture = new OpenFileDialog();
            string returnString = "";

            loadPicture.Filter = "Image Files( *.jpg; *.jpeg; *.png)| *.jpg; *.jpeg; *.png";

            if (loadPicture.ShowDialog() == DialogResult.OK)
            {
                ImageConverter converter = new ImageConverter();

                if (ValidFile(loadPicture.FileName, 150000))
                {
                    Bitmap image = new Bitmap(loadPicture.FileName); // LOAD IMAGE

                    returnString = Convert.ToBase64String((byte[])converter.ConvertTo(image, typeof(byte[]))); // TURN IMAGE INTO STRING
                }
                else
                {
                    MessageBox.Show("Error.\nMax size: 150kb.");
                }
            }

            return returnString;
        }

        // * * * * * * * * * *

        public static bool ValidFile(string filename, long limitInBytes, int limitWidth, int limitHeight)
        {
            var fileSizeInBytes = new FileInfo(filename).Length;
            if (fileSizeInBytes > limitInBytes)
            {
                return false;
            }

            using (var img = new Bitmap(filename))
            {
                if (img.Width > limitWidth || img.Height > limitHeight) return false;
            }

            return true;
        }

        // * * * * * * * * * *

        public static bool ValidFile(string filename, long limitInBytes)
        {
            var fileSizeInBytes = new FileInfo(filename).Length;
            if (fileSizeInBytes > limitInBytes)
            {
                return false;
            }

            return true;
        }
    }
}
