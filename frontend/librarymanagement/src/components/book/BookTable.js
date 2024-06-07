import React, { useState, useEffect, useContext } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { Table, Spin, Button, Modal, Form, Input, Select } from 'antd';
import { UploadOutlined } from '@ant-design/icons';
import { BookContext } from '../../context/BookContext';
import Pagination from './Pagination';

const BookTable = () => {
    const { books, loading, deleteBook } = useContext(BookContext);
    const [categories, setCategories] = useState({});
    const [form] = Form.useForm();
    const navigate = useNavigate();

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const response = await axios.get('/categories');
                const categoryMap = {};
                response.data.items.forEach(category => {
                    categoryMap[category.id] = category.name;
                });
                setCategories(categoryMap);
            } catch (error) {
                console.error('Error fetching categories:', error);
            }
        };

        fetchCategories();
    }, []);

    const handleCreateBook = () => {
        navigate('/create-book');
    };

    const handleEditBook = (book) => {
        navigate(`/edit-book/${book.id}`, { state: { image: book.image } });
    };

    const handleDeleteBook = (bookId) => {
        Modal.confirm({
            title: 'Remove this book?',
            content: 'Do you want to remove this book?',
            onOk: () => {
                deleteBook(bookId);
            },
        });
    };

    const columns = [
        {
            title: 'Title',
            dataIndex: 'title',
            key: 'title',
            render: (text) => <span style={{ color: 'red', fontWeight: 'bold' }}>{text}</span>,
        },
        {
            title: 'Author',
            dataIndex: 'author',
            key: 'author',
        },
        {
            title: 'Description',
            dataIndex: 'description',
            key: 'description',
            width: '30%',
        },
        {
            title: 'Category',
            dataIndex: 'categoryId',
            key: 'categoryId',
            render: (categoryId) => categories[categoryId] || 'Unknown',
        },
        {
            title: 'Image',
            dataIndex: 'image',
            key: 'image',
            render: (text) => <img src={text} alt="Book" style={{ width: 50 }} />,
        },
        {
            title: 'Actions',
            key: 'actions',
            render: (text, record) => (
                <>

                    <Button type="primary" onClick={() => handleEditBook(record)} style={{ marginRight: 8 }}>
                        Edit
                    </Button>
                    <Button style={{ color: 'red' }} onClick={() => handleDeleteBook(record.id)}>
                        Delete
                    </Button>
                </>
            ),
        },
    ];

    if (loading) {
        return <Spin />;
    }

    return (
        <>
            <Button type="primary" onClick={handleCreateBook} style={{ marginBottom: 16 }}>
                Create Book
            </Button>
            <Table
                dataSource={books}
                columns={columns}
                rowKey="id"
                pagination={false}
            />
            <Pagination />
        </>
    );
};

export default BookTable;
