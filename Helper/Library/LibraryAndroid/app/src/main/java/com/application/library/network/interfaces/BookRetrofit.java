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

/**
 * Доступ к книгам на сервере
 */
public interface BookRetrofit {

    /**
     * Получить список
     *
     * @return
     */
    @GET("books")
    Call<List<Book>> getBooks();

    /**
     * Добавить
     *
     * @param book
     * @return
     */
    @POST("books")
    Call<Book> add(@Body Book book);

    /**
     * Изменить
     *
     * @param id
     * @param book
     * @return
     */
    @PUT("books/{id}")
    Call<Book> update(@Path("id") int id, @Body Book book);

    /**
     * Удалить
     *
     * @param id
     * @return
     */
    @DELETE("books/{id}")
    Call<Book> delete(@Path("id") int id);

}
