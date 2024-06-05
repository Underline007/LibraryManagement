import React, { useState, useEffect } from 'react';
import { Form, Input, Button, DatePicker, message, Select } from 'antd';
import axios from 'axios';
import { useAuth } from '../context/AuthContext';

const { Option } = Select;

const BorrowBookForm = () => {
    const { user } = useAuth();
    const [loading, setLoading] = useState(false);
    const [books, setBooks] = useState([]);
    const [selectedBooks, setSelectedBooks] = useState([]);

    useEffect(() => {
        fetchBooks();
    }, []);

    const fetchBooks = async () => {
        try {
            const response = await axios.get('https://localhost:7049/api/books');
            setBooks(response.data.items);
        } catch (error) {
            console.error('Error fetching books:', error);
            message.error('Failed to fetch books');
        }
    };

    const handleSubmit = async (values) => {
        setLoading(true);
        try {
            const response = await axios.post('https://localhost:7049/api/bookborrowingrequests', { ...values, bookIds: selectedBooks });
            console.log('Request created:', response.data);
            message.success('Request created successfully');
        } catch (error) {
            console.error('Error creating request:', error);
            message.error('Failed to create request');
        } finally {
            setLoading(false);
        }
    };

    return (
        <Form layout="vertical" onFinish={handleSubmit}>
            <Form.Item
                name="requestorId"
                label="Requestor ID"
                initialValue={user.id}
                hidden // Ẩn trường Requestor ID
            >
                <Input disabled />
            </Form.Item>
            <Form.Item
                name="bookBorrowingReturnDate"
                label="Return Date"
                rules={[{ required: true, message: 'Please select return date' }]}
            >
                <DatePicker />
            </Form.Item>
            <Form.Item
                name="bookIds"
                label="Select Books"
                rules={[{ required: true, message: 'Please select at least one book' }]}
            >
                <Select
                    mode="multiple"
                    showSearch
                    filterOption={(input, option) =>
                        option.children.toLowerCase().indexOf(input.toLowerCase()) >= 0
                    }
                    onChange={(values) => setSelectedBooks(values)}
                >
                    {books.map(book => (
                        <Option key={book.id} value={book.id}>{book.title}</Option>
                    ))}
                </Select>
            </Form.Item>
            <Button type="primary" htmlType="submit" loading={loading}>Borrow Books</Button>
        </Form>
    );
};

export default BorrowBookForm;
