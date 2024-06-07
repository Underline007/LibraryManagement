import React, { createContext, useState, useContext, useEffect } from 'react';
import axios from 'axios';
import { message } from 'antd';
import { useAuth } from './AuthContext';

const CategoryContext = createContext();

export const useCategoryContext = () => useContext(CategoryContext);

export const CategoryProvider = ({ children }) => {
    const { token } = useAuth();
    const [categories, setCategories] = useState([]);
    const [totalPages, setTotalPages] = useState(0);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        fetchCategories();
    }, []);

    const fetchCategories = async (pageIndex = 0, pageSize = 10) => {
        try {
            setLoading(true);
            const response = await axios.get(`/categories`, {
                params: {
                    pageIndex,
                    pageSize
                },
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setCategories(response.data.items);
            setTotalPages(response.data.totalPages);
            setLoading(false);
        } catch (error) {
            console.error('Error fetching categories:', error);
            setLoading(false);
        }
    };

    const addCategory = async (category) => {
        try {
            const response = await axios.post('/categories', category, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setCategories([...categories, response.data]);
        } catch (error) {
            console.error('Error adding category:', error);
        }
    };

    const updateCategory = async (categoryId, updatedCategory) => {
        try {
            const response = await axios.put(`/categories/${categoryId}`, updatedCategory, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            const updatedCategories = categories.map(cat => (cat.id === categoryId ? response.data : cat));
            setCategories(updatedCategories);
            message.success("Category Edited");
            fetchCategories();
        } catch (error) {
            console.error('Error updating category:', error);
        }
    };

    const deleteCategory = async (categoryId) => {
        try {
            await axios.delete(`/categories/${categoryId}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            const filteredCategories = categories.filter(cat => cat.id !== categoryId);
            setCategories(filteredCategories);
        } catch (error) {
            console.error('Error deleting category:', error);
        }
    };

    return (
        <CategoryContext.Provider
            value={{ categories, totalPages, loading, fetchCategories, addCategory, updateCategory, deleteCategory }}
        >
            {children}
        </CategoryContext.Provider>
    );
};
