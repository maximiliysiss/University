package com.application.library.network.interfaces;

import com.application.library.network.models.input.Book;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface BookRetrofit {

    @GET("books")
    Call<List<Book>> getBooks();

    @POST("books")
    Call<Book> add(@Body Book book);

    @PUT("books/{id}")
    Call<Book> update(@Path("id") int id, @Body Book book);

    @DELETE("books/{id}")
    Call<Book> delete(@Path("id") int id);

}
