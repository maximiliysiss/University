package com.example.laboratory_2.services;

import android.util.Pair;
import java.util.ArrayDeque;
import java.util.HashMap;

public class Parser {

    private Converter converter = new Converter();

    public  interface Operation{
        Double operation(Double f, Double s);
    }

    public interface Filter{
        CharSequence FilterSeq(CharSequence charSequence);
    }

    private Filter numberFilter = new Filter() {
        @Override
        public CharSequence FilterSeq(CharSequence charSequence) {
            StringBuilder stringBuilder = new StringBuilder();
            for(int it=0;it<charSequence.length();it++){
                String symbol = Character.toString(charSequence.charAt(it));
                if(Character.isDigit(charSequence.charAt(it)) || operations.keySet().contains(symbol) || symbol.equals(".")){
                    stringBuilder.append(symbol);
                }
            }
            return stringBuilder.toString();
        }
    };

    public Filter getNumberFilter(){
        return numberFilter;
    }

    HashMap<String, Operation> operations = new HashMap<>();
    HashMap<String, Integer> exprOperations = new HashMap<>();

    public Operation getOperation(String name){
        return operations.get(name);
    }

    public Parser(){

        exprOperations.put("+",0);
        exprOperations.put("-",0);
        exprOperations.put("*",1);
        exprOperations.put("/",1);

        operations.put("+", new Operation() {
            @Override
            public Double operation(Double f, Double s) {
                return f+s;
            }
        });

        operations.put("-", new Operation() {
            @Override
            public Double operation(Double f, Double s) {
                return f-s;
            }
        });

        operations.put("*", new Operation() {
            @Override
            public Double operation(Double f, Double s) {
                return f*s;
            }
        });

        operations.put("/", new Operation() {
            @Override
            public Double operation(Double f, Double s) {
                return f/s;
            }
        });

    }

    String erasePart(String str, int begin, int end){
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(str.substring(0, begin));
        stringBuilder.append(str.substring(end + 1));
        return stringBuilder.toString();
    }

    String insertPart(String dest, String source, int pos){
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(dest.substring(0,pos));
        stringBuilder.append(source);
        stringBuilder.append(dest.substring(pos));
        return stringBuilder.toString();
    }

    public Double parsing(String expr){

        ArrayDeque<Integer> opersPositions = getOperationsIndex(expr);
        if(opersPositions.isEmpty())
            return converter.tryParseDouble(expr);

        Double result = 0.0;

        while(!opersPositions.isEmpty()){
            Integer pos = opersPositions.pop();
            Pair<Double, Integer> left = getNumberFromString(expr, pos, false);
            Pair<Double, Integer> right = getNumberFromString(expr, pos, true);
            result = operations.get(Character.toString(expr.charAt(pos))).operation(left.first, right.first);
            String newNum = result.toString();
            expr = erasePart(expr, left.second, right.second);
            expr = insertPart(expr, newNum, left.second);
            opersPositions = getOperationsIndex(expr);
        }
        return result;
    }

    Pair<Double,Integer> getNumberFromString(String str, Integer pos, boolean direction){
        StringBuilder stringBuilder = new StringBuilder();
        int dir = direction? 1:-1;

        int i=pos+dir;
        for(;(direction?i<str.length():i>=0); i+=dir){

            String ch = Character.toString(str.charAt(i));
            if(i!=0 && i!=pos+1 && !Character.isDigit(str.charAt(i)) && !ch.equals("."))
                break;

            stringBuilder.append(ch);
        }

        if(!direction)
            stringBuilder = stringBuilder.reverse();

        return new Pair<>(converter.tryParseDouble(stringBuilder.toString()), i + (direction? -1:+1));
    }

    private ArrayDeque<Integer> getOperationsIndex(String expr){
        ArrayDeque<Integer> opersPositions = new ArrayDeque<>();
        for(int i=0;i<expr.length();i++){
            if(exprOperations.keySet().contains(Character.toString(expr.charAt(i))) && i!=0){
                Integer val = exprOperations.get(Character.toString(expr.charAt(i)));
                if(val==1)
                    opersPositions.addFirst(i);
                else
                    opersPositions.addLast(i);
            }
        }
        return opersPositions;
    }

}
