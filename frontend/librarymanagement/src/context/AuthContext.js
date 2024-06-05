import React, { createContext, useState, useEffect, useContext } from 'react';
import axios from 'axios';
import { jwtDecode } from "jwt-decode";
import { useNavigate } from 'react-router-dom';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState({});
    const [token, setToken] = useState(null);
    const navigate = useNavigate();

    const login = async (email, password) => {
        try {
            const response = await axios.post('https://localhost:7049/api/auth/login', {
                email,
                password,
            });
            setUser(response.data);
            setToken(response.data.token);
            if (response.data.role === 1) {
                await navigate('/dashboard');
            } else {
                await navigate('/book');
            }
            localStorage.setItem('token', response.data.token);
        } catch (error) {
            console.error('Login error:', error);
            throw error;
        }
    };

    const logout = () => {
        setUser({});
        setToken(null);
        localStorage.removeItem('token');
    };

    useEffect(() => {
        const storedToken = localStorage.getItem('token');
        if (storedToken) {

            setToken(storedToken);

            const decodedToken = jwtDecode(storedToken);
            const userId = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            fetchUserInfo(userId);
        }
    }, []);

    const fetchUserInfo = async (userId) => {
        try {
            const response = await axios.get(`https://localhost:7049/api/users/${userId}`);
            setUser(response.data);
        } catch (error) {
            console.error('Error fetching user info:', error);
        }
    };

    return (
        <AuthContext.Provider value={{ user, token, login, logout, setUser, setToken }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    return useContext(AuthContext);
};
