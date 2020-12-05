package com.server.common;

import javax.crypto.*;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;
import java.math.BigInteger;
import java.nio.charset.StandardCharsets;
import java.security.InvalidAlgorithmParameterException;
import java.security.InvalidKeyException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class Cryptographic {

    private Cipher cipher;
    private byte[] keyBytes;
    private byte[] vector;

    private Cryptographic(String key) {
        this.keyBytes = vector = key.getBytes();
        SecretKeySpec des = new SecretKeySpec(keyBytes, "DES");
        IvParameterSpec ivSpec = new IvParameterSpec(vector);

        cipher = null;
        try {
            cipher = Cipher.getInstance("DES/CBC/PKCS5Padding");
            cipher.init(Cipher.ENCRYPT_MODE, des, ivSpec);
        } catch (NoSuchAlgorithmException | NoSuchPaddingException | InvalidKeyException | InvalidAlgorithmParameterException e) {
            e.printStackTrace();
        }
    }

    public String encrypt(String inputString) {
        try {
            byte[] input = inputString.getBytes();
            byte[] encrypted = new byte[cipher.getOutputSize(input.length)];
            int enc_len = cipher.update(input, 0, input.length, encrypted, 0);
            enc_len += cipher.doFinal(encrypted, enc_len);
            return new String(encrypted, StandardCharsets.UTF_8);
        } catch (ShortBufferException | BadPaddingException | IllegalBlockSizeException e) {
            e.printStackTrace();
        }
        return null;
    }

    public String decrypt(String inputString) {
        try {
            byte[] encrypted = inputString.getBytes();
            byte[] decrypted = new byte[cipher.getOutputSize(encrypted.length)];
            int dec_len = cipher.update(encrypted, 0, encrypted.length, decrypted, 0);
            dec_len += cipher.doFinal(decrypted, dec_len);
            return new String(decrypted, StandardCharsets.UTF_8);
        } catch (BadPaddingException | IllegalBlockSizeException | ShortBufferException e) {
            e.printStackTrace();
        }
        return null;
    }

    public String md5(String st) {
        MessageDigest messageDigest = null;
        byte[] digest = new byte[0];

        try {
            messageDigest = MessageDigest.getInstance("MD5");
            messageDigest.reset();
            messageDigest.update(st.getBytes());
            digest = messageDigest.digest();
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        }

        BigInteger bigInt = new BigInteger(1, digest);
        String md5Hex = bigInt.toString(16);

        while (md5Hex.length() < 32) {
            md5Hex = "0" + md5Hex;
        }

        return md5Hex;
    }

    private static Cryptographic cryptographic;

    public static void init(String key) {
        Cryptographic.cryptographic = new Cryptographic(key);
    }

    public static Cryptographic get() {
        if (cryptographic == null)
            throw new NullPointerException("Not found instance");
        return cryptographic;
    }
}
