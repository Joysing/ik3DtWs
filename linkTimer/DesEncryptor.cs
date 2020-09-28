using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using javax.crypto.spec;
using javax.crypto;
using java.lang;

namespace linkTimer
{
    public class DesEncryptor
    {
        private string pwd { get; set; }
        private string iv { get; set; }

        public DesEncryptor(string pwd, string iv)
        {
            this.pwd = pwd;
            this.iv = iv;
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="param">需要加密的字符串</param>
        /// <returns></returns>
        public string encode(string param)
        {
            DESKeySpec localDESKeySpec = new DESKeySpec(Encoding.UTF8.GetBytes(pwd));
            SecretKeyFactory localSecretKeyFactory = SecretKeyFactory.getInstance("DES");
            SecretKey localSecretKey = localSecretKeyFactory.generateSecret(localDESKeySpec);
            IvParameterSpec localIvParameterSpec = new IvParameterSpec(Encoding.UTF8.GetBytes(iv));

            Cipher localCipher = Cipher.getInstance("DES/CBC/PKCS5Padding");
            localCipher.init(1, localSecretKey, localIvParameterSpec);
		    return parseByte2HexStr(localCipher.doFinal(Encoding.UTF8.GetBytes(param)));
	    }

        /**
	     * 转16进制字符
	     */
        public string parseByte2HexStr(byte[] paramArrayOfByte)
        {
            StringBuffer localStringBuffer = new StringBuffer();
            for (int i = 0; i < paramArrayOfByte.Length; i++)
            {
                string str = Integer.toHexString(paramArrayOfByte[i] & 0xFF);
                if (str.Length == 1)
                {
                    str = '0' + str;
                }
                localStringBuffer.append(str.ToUpper());
            }
            return localStringBuffer.toString();
        }
        //3DES的cbc加密[24位密钥对应192位加密]
        public static string TripleDesEncryptorCBC(string text, string key, string iv)
        {
            var tripleDESCipher = new TripleDESCryptoServiceProvider();
            tripleDESCipher.Mode = CipherMode.CBC;
            tripleDESCipher.Padding = PaddingMode.PKCS7;
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[24];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
                len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            tripleDESCipher.Key = keyBytes;
            tripleDESCipher.IV = Encoding.ASCII.GetBytes(iv);

            ICryptoTransform transform = tripleDESCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(text);
            byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
            return Convert.ToBase64String(cipherBytes);
        }

        //3DES的cbc解密
        public static string TripleDesDecryptorCBC(string text, string key, string iv)
        {
            var tripleDESCipher = new TripleDESCryptoServiceProvider();
            tripleDESCipher.Mode = CipherMode.CBC;
            tripleDESCipher.Padding = PaddingMode.PKCS7;

            byte[] encryptedData = Convert.FromBase64String(text);
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[24];
            byte[] ivBytes = Encoding.ASCII.GetBytes(iv);
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
                len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            tripleDESCipher.Key = keyBytes;
            tripleDESCipher.IV = ivBytes;
            ICryptoTransform transform = tripleDESCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }


        //加密
        public string DESEncrypt(string paymentCode, string key, string iv)
        {
            SymmetricAlgorithm symmetric;
            ICryptoTransform iCrypto;
            MemoryStream memory;
            CryptoStream crypto;
            byte[] byt;
            symmetric = new TripleDESCryptoServiceProvider();
            symmetric.Key = Encoding.UTF8.GetBytes(key);
            symmetric.IV = Encoding.UTF8.GetBytes(iv);
            iCrypto = symmetric.CreateEncryptor();
            byt = Encoding.UTF8.GetBytes(paymentCode);
            memory = new MemoryStream();
            crypto = new CryptoStream(memory, iCrypto, CryptoStreamMode.Write);
            crypto.Write(byt, 0, byt.Length);
            crypto.FlushFinalBlock();
            crypto.Close();
            return Convert.ToBase64String(memory.ToArray());
        }

        //解密
        public static string DESDecrypst(string data, string key, string iv)
        {
            SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();

            mCSP.Key = Encoding.UTF8.GetBytes(key);
            mCSP.IV = Encoding.UTF8.GetBytes(iv);
            ICryptoTransform iCrypto;
            MemoryStream memory;
            CryptoStream crypto;
            byte[] byt;
            iCrypto = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);

            byt = Convert.FromBase64String(data);
            memory = new MemoryStream();
            crypto = new CryptoStream(memory, iCrypto, CryptoStreamMode.Write);
            crypto.Write(byt, 0, byt.Length);
            crypto.FlushFinalBlock();
            crypto.Close();
            return Encoding.UTF8.GetString(memory.ToArray());
        }

    }
}
