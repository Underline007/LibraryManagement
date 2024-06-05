import React, { createContext, useState, useEffect, useContext } from 'react';
import axios from 'axios';
import { message } from 'antd';

export const BookContext = createContext();

export const BookProvider = ({ children }) => {
    const [books, setBooks] = useState([]);
    const [loading, setLoading] = useState(true);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10); // Thêm state cho pageSize
    const [totalPages, setTotalPages] = useState(1);

    useEffect(() => {
        const fetchBooks = async () => {
            setLoading(true);
            try {
                const response = await axios.get(`https://localhost:7049/api/books?pageNumber=${pageNumber}&pageSize=${pageSize}`);
                setBooks(response.data.items);
                setTotalPages(response.data.totalPages);
            } catch (error) {
                console.error('Error fetching books:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchBooks();
    }, [pageNumber, pageSize]);

    const changePage = (newPageNumber) => {
        if (newPageNumber > 0 && newPageNumber <= totalPages) {
            setPageNumber(newPageNumber);
        }
    };

    // Hàm để cập nhật pageSize
    const updatePageSize = (newPageSize) => {
        setPageSize(newPageSize);
        setPageNumber(1); // Đặt lại pageNumber về 1 khi thay đổi pageSize
    };

    const createBook = async (newBook) => {
        try {
            const response = await axios.post('https://localhost:7049/api/books', newBook);

            setBooks([...books, response.data]);
            message.success("Create book successfully");
        } catch (error) {
            console.error('Error creating book:', error);
            message.error("Error while create book");

        }
    };

    const editBook = async (updatedBook) => {
        try {
            await axios.put(`https://localhost:7049/api/books/${updatedBook.id}`, updatedBook);
            setBooks(books.map(book => (book.id === updatedBook.id ? updatedBook : book)));
            message.success("Edit book successfully");
        } catch (error) {
            console.error('Error editing book:', error);
            message.error("Error while edit book");
        }
    };

    const deleteBook = async (bookId) => {
        try {
            await axios.delete(`https://localhost:7049/api/books/${bookId}`);
            setBooks(books.filter(book => book.id !== bookId));
            message.success("Delete book successfully");
        } catch (error) {
            console.error('Error deleting book:', error);
            message.error("Error while delete book");

        }
    };

    return (
        <BookContext.Provider value={{ books, loading, pageNumber, pageSize, totalPages, changePage, createBook, editBook, deleteBook, updatePageSize }}>
            {children}
        </BookContext.Provider>
    );
};

export const useBookContext = () => {
    return useContext(BookContext);
};
