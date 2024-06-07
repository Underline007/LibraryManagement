
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
import ManageRequests from './pages/ManageRequest';
import UserRequests from './pages/UserRequests';
import HomePage from './pages/Homepage';
import PageNotFound from './pages/PageNotFound';
import NotAuthorized from './pages/NotAuthorized';
import { useAuth } from './context/AuthContext';

const AppRoutes = () => {
    const { user } = useAuth();
    return (
        <Routes>

            <Route path="/login" element={<Login />} />
            <Route path="/signup" element={<Signup />} />
            <Route path="/books/:id" element={<BookDetail />} />
            <Route path="/*" element={<PageNotFound />} />
            <Route path="/not-authorized" element={<NotAuthorized />} />
            <Route path="/" element={<HomePage />} />
            {user.role === 0 && (
                <>
                    <Route path="/borrowing-book" element={<BorrowBookForm />} />
                    <Route path="/manage-request/:id" element={<UserRequests />} />
                </>
            )}
            {user.role === 1 && (
                <>
                    <Route path="/create-book" element={<CreateBookPage />} />
                    <Route path="/edit-book/:id" element={<EditBookPage />} />
                    <Route path="/manage-request" element={<ManageRequests />} />
                    <Route path="/manage-book" element={<BookTable />} />
                    <Route path="/categories" element={<CategoryList />} />
                </>
            )}


        </Routes>
    );
};

export default AppRoutes;
