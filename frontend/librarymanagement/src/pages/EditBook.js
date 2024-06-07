import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Form, Input, Button, Select } from 'antd';
import { useParams } from 'react-router-dom';
import { useDropzone } from 'react-dropzone';

const { Option } = Select;

const EditBookPage = () => {
    const [book, setBook] = useState(null);
    const [categories, setCategories] = useState([]);
    const [loading, setLoading] = useState(false);
    const [image, setImage] = useState(null);
    const { id } = useParams();

    useEffect(() => {
        const fetchBook = async () => {
            try {
                const response = await axios.get(`/books/${id}`);
                setBook(response.data);
            } catch (error) {
                console.error('Error fetching book:', error);
            }
        };

        fetchBook();
    }, [id]);

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const response = await axios.get('/categories');
                setCategories(response.data.items);
            } catch (error) {
                console.error('Error fetching categories:', error);
            }
        };

        fetchCategories();
    }, []);

    useEffect(() => {
        if (book && !image) {
            setImage(book.image);
        }
    }, [book, image]);

    const handleSubmit = async (values) => {
        setLoading(true);
        try {
            const formData = new FormData();
            formData.append('title', values.title);
            formData.append('author', values.author);
            formData.append('description', values.description);
            formData.append('categoryId', values.categoryId);
            if (image) {
                formData.append('image', image);
            }

            await axios.put(`/books/${book.id}`, formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });

            window.location.href = '/books';
        } catch (error) {
            console.error('Error editing book:', error);
            setLoading(false);
        }
    };

    const { getRootProps, getInputProps } = useDropzone({
        accept: 'image/*',
        onDrop: acceptedFiles => {
            setImage(acceptedFiles[0]);
        }
    });

    if (!book) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <h1>Edit Book</h1>
            <Form
                initialValues={book}
                onFinish={handleSubmit}
                layout="vertical"
            >
                <Form.Item
                    name="title"
                    label="Title"
                    rules={[{ required: true, message: 'Please enter a title' }]}
                >
                    <Input />
                </Form.Item>
                <Form.Item
                    name="author"
                    label="Author"
                    rules={[{ required: true, message: 'Please enter an author' }]}
                >
                    <Input />
                </Form.Item>
                <Form.Item
                    name="description"
                    label="Description"
                    rules={[{ required: true, message: 'Please enter a description' }]}
                >
                    <Input.TextArea />
                </Form.Item>
                <Form.Item
                    name="categoryId"
                    label="Category"
                    rules={[{ required: true, message: 'Please select a category' }]}
                >
                    <Select>
                        {categories.map(category => (
                            <Option key={category.id} value={category.id}>{category.name}</Option>
                        ))}
                    </Select>
                </Form.Item>
                <Form.Item
                    label="Upload Image"
                    style={{
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                        padding: '20px',
                        border: '2px dashed #ccc',
                        borderRadius: '4px',
                        cursor: 'pointer',
                        transition: 'all 0.3s ease',
                        '&:hover': {
                            backgroundColor: '#f5f5f5',
                            borderColor: '#1890ff',
                        },
                    }}
                >
                    <div
                        {...getRootProps()}
                        style={{
                            display: 'flex',
                            flexDirection: 'column',
                            alignItems: 'center',
                            justifyContent: 'center',
                            width: '100%',
                            height: '40px',
                        }}
                    >
                        <input {...getInputProps()} />
                        <p
                            style={{
                                color: '#666',
                                fontSize: '16px',
                                fontWeight: 'bold',
                                marginTop: '10px',
                            }}
                        >
                            Drag 'n' drop an image here, or click to select an image
                        </p>
                    </div>
                </Form.Item>
                <Form.Item>
                    <Button type="primary" htmlType="submit" loading={loading}>
                        Save
                    </Button>
                </Form.Item>
            </Form>
        </div>
    );
};

export default EditBookPage;
