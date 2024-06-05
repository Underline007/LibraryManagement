// Routes.js
import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Login from './components/auth/Login';
import Signup from './components/auth/Signup';
import BookTable from './components/book/BookTable';
import CategoryList from './pages/CategoryList';
import BookDetail from './pages/BookDetail';
import CreateBookPage from './pages/CreateBook';
import EditBookPage from './pages/EditBook';
import BorrowBookForm from './pages/BorrowBookForm';

const AppRoutes = () => {
    return (
        <Routes>
            <Route path="/login" element={<Login />} />
            <Route path="/signup" element={<Signup />} />
            <Route path="/books/:id" element={<BookDetail />} />
            <Route path="/create-book" element={<CreateBookPage />} />
            <Route path="/edit-book/:id" element={<EditBookPage />} />
            <Route path="/borrowing-book" element={<BorrowBookForm />} />
            <Route path="/" element={<BookTable />} />
            <Route path="/categories" element={<CategoryList />} />
        </Routes>
    );
};

export default AppRoutes;
