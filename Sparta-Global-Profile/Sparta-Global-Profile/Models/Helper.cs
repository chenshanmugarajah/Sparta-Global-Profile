using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public static class Helper
    {
        //public static string ToAbsoluteUrl(this string relativeUrl) //Use absolute URL instead of adding phycal path for CSS, JS and Images     
        //{
        //    if (string.IsNullOrEmpty(relativeUrl)) return relativeUrl;
        //    if (HttpContext.Current == null) return relativeUrl;
        //    if (relativeUrl.StartsWith("/")) relativeUrl = relativeUrl.Insert(0, "~");
        //    if (!relativeUrl.StartsWith("~/")) relativeUrl = relativeUrl.Insert(0, "~/");
        //    var url = HttpContext.Current.Request.Url;
        //    var port = url.Port != 80 ? (":" + url.Port) : String.Empty;
        //    return String.Format("{0}://{1}{2}{3}", url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));
        //}


        private const string SecurityKey = "ComplexKeyHere_12121";

        public static string EncryptPlainTextToCipherText(string PlainText)
        {
            /*Getting the bytes of Input String.*/
            byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(PlainText);
            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();
            //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value. 
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey)); //De-allocatinng the memory after doing the Job. 
            objMD5CryptoService.Clear(); var objTripleDESCryptoService = new TripleDESCryptoServiceProvider(); //Assigning the Security key to the TripleDES Service Provider. 
            objTripleDESCryptoService.Key = securityKeyArray; //Mode of the Crypto service is Electronic Code Book. 
            objTripleDESCryptoService.Mode = CipherMode.ECB; //Padding Mode is PKCS7 if there is any extra byte is added. 
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7; var objCrytpoTransform = objTripleDESCryptoService.CreateEncryptor(); 
            //Transform the bytes array to resultArray 
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length); objTripleDESCryptoService.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string DecryptCipherTextToPlainText(string CipherText)
        {
            byte[] toEncryptArray = Convert.FromBase64String(CipherText);
            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider(); //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value. 
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
            objMD5CryptoService.Clear();
            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider(); //Assigning the Security key to the TripleDES Service Provider. 
            objTripleDESCryptoService.Key = securityKeyArray; //Mode of the Crypto service is Electronic Code Book. 
            objTripleDESCryptoService.Mode = CipherMode.ECB; //Padding Mode is PKCS7 if there is any extra byte is added. 
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;
            var objCrytpoTransform = objTripleDESCryptoService.CreateDecryptor(); //Transform the bytes array to resultArray 
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            objTripleDESCryptoService.Clear(); //Convert and return the decrypted data/byte into string format. 
            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        //public static string GeneratePassword(int length) //length of salt    
        //{
        //    const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
        //    var randNum = new Random();
        //    var chars = new char[length];
        //    var allowedCharCount = allowedChars.Length;
        //    for (var i = 0; i <= length - 1; i++)
        //    {
        //        chars[i] = allowedChars[Convert.ToInt32((allowedChars.Length) * randNum.NextDouble())];
        //    }
        //    return new string(chars);
        //}
        //public static string EncodePassword(string pass, string salt) //encrypt password    
        //{
        //    byte[] bytes = System.Text.Encoding.Unicode.GetBytes(pass);
        //    byte[] src = Encoding.Unicode.GetBytes(salt);
        //    byte[] dst = new byte[src.Length + bytes.Length];
        //    System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
        //    System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
        //    HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
        //    byte[] inArray = algorithm.ComputeHash(dst);
        //    //return Convert.ToBase64String(inArray);    
        //    return EncodePasswordMd5(Convert.ToBase64String(inArray));
        //}
        //public static string EncodePasswordMd5(string pass) //Encrypt using MD5    
        //{
        //    Byte[] originalBytes;
        //    Byte[] encodedBytes;
        //    MD5 md5;
        //    //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
        //    md5 = new MD5CryptoServiceProvider();
        //    originalBytes = ASCIIEncoding.Default.GetBytes(pass);
        //    encodedBytes = md5.ComputeHash(originalBytes);
        //    //Convert encoded bytes back to a 'readable' string    
        //    return BitConverter.ToString(encodedBytes);
        //}
        //public static string base64Encode(string sData) // Encode    
        //{
        //    try
        //    {
        //        byte[] encData_byte = new byte[sData.Length];
        //        encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
        //        string encodedData = Convert.ToBase64String(encData_byte);
        //        return encodedData;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error in base64Encode" + ex.Message);
        //    }
        //}
        //public static string base64Decode(string sData) //Decode    
        //{
        //    try
        //    {
        //        var encoder = new System.Text.UTF8Encoding();
        //        System.Text.Decoder utf8Decode = encoder.GetDecoder();
        //        byte[] todecodeByte = Convert.FromBase64String(sData);
        //        int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
        //        char[] decodedChar = new char[charCount];
        //        utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
        //        string result = new String(decodedChar);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error in base64Decode" + ex.Message);
        //    }
        //}
    }
}
