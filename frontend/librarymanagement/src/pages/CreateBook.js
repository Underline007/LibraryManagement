import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Form, Input, Button, Select } from 'antd';
import { useDropzone } from 'react-dropzone';

const { Option } = Select;

const CreateBookPage = ({ history }) => {
    const [categories, setCategories] = useState([]);
    const [loading, setLoading] = useState(false);
    const [image, setImage] = useState(null);

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const response = await axios.get('https://localhost:7049/api/categories');
                setCategories(response.data.items);
            } catch (error) {
                console.error('Error fetching categories:', error);
            }
        };

        fetchCategories();
    }, []);

    const handleSubmit = async (values) => {
        setLoading(true);
        try {
            // Gửi dữ liệu tạo sách mới đến API
            const formData = new FormData();
            formData.append('title', values.title);
            formData.append('author', values.author);
            formData.append('description', values.description);
            formData.append('categoryId', values.categoryId);
            formData.append('image', image);

            await axios.post('https://localhost:7049/api/books', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });

            // Chuyển hướng về trang danh sách sách sau khi tạo thành công
            window.location.href = '/';
        } catch (error) {
            console.error('Error creating book:', error);
            setLoading(false);
        }
    };

    const { getRootProps, getInputProps } = useDropzone({
        accept: 'image/*',
        onDrop: acceptedFiles => {
            setImage(acceptedFiles[0]);
        }
    });

    return (
        <div>
            <h1>Create New Book</h1>
            <Form
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
                <Form.Item label="Upload Image">
                    <div {...getRootProps()}>
                        <input {...getInputProps()} />
                        <p>Drag 'n' drop an image here, or click to select an image</p>
                    </div>
                </Form.Item>
                <Form.Item>
                    <Button type="primary" htmlType="submit" loading={loading}>
                        Create
                    </Button>
                </Form.Item>
            </Form>
        </div>
    );
};

export default CreateBookPage;
