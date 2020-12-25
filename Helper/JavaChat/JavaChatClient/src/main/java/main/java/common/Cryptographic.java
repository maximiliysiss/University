package main.java.common;

import javax.crypto.*;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;
import java.math.BigInteger;
import java.nio.charset.StandardCharsets;
import java.security.InvalidAlgorithmParameterException;
import java.security.InvalidKeyException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

/**
 * Класс для шифрования
 */
public class Cryptographic {

    /**
     * Настройки шифрования
     */
    private final SecretKeySpec des;
    private final IvParameterSpec ivSpec;
    private Cipher cipher;
    private byte[] keyBytes;
    private byte[] vector;

    private Cryptographic(String key) {
        this.keyBytes = vector = key.getBytes();
        des = new SecretKeySpec(keyBytes, "DES");
        ivSpec = new IvParameterSpec(vector);

        cipher = null;
        try {
            cipher = Cipher.getInstance("DES/CBC/PKCS5Padding");
        } catch (NoSuchAlgorithmException | NoSuchPaddingException e) {
            e.printStackTrace();
        }
    }

    /**
     * Зашифровать
     *
     * @param input
     * @return
     */
    public byte[] encrypt(byte[] input) {
        try {
            cipher.init(Cipher.ENCRYPT_MODE, des, ivSpec);
            byte[] encrypted = new byte[cipher.getOutputSize(input.length)];
            int enc_len = cipher.update(input, 0, input.length, encrypted, 0);
            enc_len += cipher.doFinal(encrypted, enc_len);
            return encrypted;
        } catch (ShortBufferException | BadPaddingException | IllegalBlockSizeException | InvalidAlgorithmParameterException | InvalidKeyException e) {
            e.printStackTrace();
        }
        return null;
    }

    /**
     * Дешифровать
     *
     * @param encrypted
     * @return
     */
    public byte[] decrypt(byte[] encrypted) {
        try {
            cipher.init(Cipher.DECRYPT_MODE, des, ivSpec);
            byte[] decrypted = new byte[cipher.getOutputSize(encrypted.length)];
            int dec_len = cipher.update(encrypted, 0, encrypted.length, decrypted, 0);
            dec_len += cipher.doFinal(decrypted, dec_len);
            return decrypted;
        } catch (BadPaddingException | IllegalBlockSizeException | ShortBufferException | InvalidAlgorithmParameterException | InvalidKeyException e) {
            e.printStackTrace();
        }
        return null;
    }

    /**
     * Singleton паттерн
     */

    private static Cryptographic cryptographic;

    public synchronized static void init(String key) {
        Cryptographic.cryptographic = new Cryptographic(key);
    }

    public static Cryptographic get() {
        if (cryptographic == null)
            throw new NullPointerException("Not found instance");
        return cryptographic;
    }
}
