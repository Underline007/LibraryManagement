import React, { createContext, useState, useEffect, useContext } from 'react';
import axios from 'axios';
import { message } from 'antd';
import { useAuth } from './AuthContext';


export const BookContext = createContext();

export const BookProvider = ({ children }) => {
    const { token } = useAuth();
    const [books, setBooks] = useState([]);
    const [loading, setLoading] = useState(true);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(1);
    const [searchTerm, setSearchTerm] = useState('');
    const [sortBy, setSortBy] = useState('title');

    useEffect(() => {
        fetchBooks();
    }, [pageNumber, pageSize]);

    const fetchBooks = async () => {
        setLoading(true);
        try {
            const response = await axios.get("/books", {
                params: {
                    pageNumber,
                    pageSize,
                },
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setBooks(response.data.items);
            setTotalPages(response.data.totalPages);
        } catch (error) {
            console.error('Error fetching books:', error);
            message.error('Error fetching books');
        } finally {
            setLoading(false);
        }
    };

    const fetchBooksByQuery = async () => {
        setLoading(true);
        try {
            const response = await axios.get(`/books/query`, {
                headers: {
                    Authorization: `Bearer ${token}`
                },

                params: {
                    searchTerm,
                    sortBy,
                },

            });
            setBooks(response.data);
        } catch (error) {
            console.error('Error fetching books by query:', error);
            // message.error('Error while fetching books');
        } finally {
            setLoading(false);
        }
    };

    const changePage = (newPageNumber) => {
        if (newPageNumber > 0 && newPageNumber <= totalPages) {
            setPageNumber(newPageNumber);
        }
    };

    const updatePageSize = (newPageSize) => {
        setPageSize(newPageSize);
        setPageNumber(1);
    };

    const createBook = async (newBook) => {
        try {
            const response = await axios.post('/books', newBook, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setBooks([...books, response.data]);
            message.success('Create book successfully');
        } catch (error) {
            console.error('Error creating book:', error);
            message.error('Error while create book');
        }
    };

    const editBook = async (updatedBook) => {
        try {
            await axios.put(`/books/${updatedBook.id}`, updatedBook, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setBooks(books.map(book => (book.id === updatedBook.id ? updatedBook : book)));
            message.success('Edit book successfully');
        } catch (error) {
            console.error('Error editing book:', error);
            message.error('Error while edit book');
        }
    };

    const deleteBook = async (bookId) => {
        try {
            await axios.delete(`/books/${bookId}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setBooks(books.filter(book => book.id !== bookId));
            message.success('Delete book successfully');
        } catch (error) {
            console.error('Error deleting book:', error);
            message.error('Error while delete book');
        }
    };

    return (
        <BookContext.Provider value={{
            books,
            loading,
            pageNumber,
            pageSize,
            totalPages,
            changePage,
            createBook,
            editBook,
            deleteBook,
            updatePageSize,
            setSearchTerm,
            setSortBy,
            fetchBooksByQuery,
            fetchBooks,
            searchTerm,
            sortBy
        }}>
            {children}
        </BookContext.Provider>
    );
};

// Hook để sử dụng context
export const useBookContext = () => {
    return useContext(BookContext);
};
