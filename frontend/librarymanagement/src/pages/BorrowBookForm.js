import React, { useState, useEffect } from 'react';
import { Form, Button, DatePicker, message, Select, Divider, Typography } from 'antd';
import axios from 'axios';
import { useAuth } from '../context/AuthContext';

const { Option } = Select;
const { Title } = Typography;

const formItemLayout = {
    labelCol: {
        xs: { span: 24 },
        sm: { span: 8 },
    },
    wrapperCol: {
        xs: { span: 24 },
        sm: { span: 16 },
    },
};

const titleStyle = {
    textAlign: 'center',
    marginBottom: '30px',
    color: '#1890ff',
};

const formStyle = {
    maxWidth: '600px',
    margin: '0 auto',
    padding: '30px',
    backgroundColor: '#fff',
    boxShadow: '0 2px 8px rgba(0, 0, 0, 0.15)',
    borderRadius: '4px',
};

const buttonStyle = {
    marginTop: '20px',
};

const BorrowBookForm = () => {
    const { token, user } = useAuth();
    const [loading, setLoading] = useState(false);
    const [books, setBooks] = useState([]);
    const [selectedBooks, setSelectedBooks] = useState([]);

    useEffect(() => {
        fetchBooks();
    }, []);

    const fetchBooks = async () => {
        try {
            const response = await axios.get('/books', {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setBooks(response.data.items);
        } catch (error) {
            console.error('Error fetching books:', error);
            message.error('Failed to fetch books');
        }
    };

    const handleSubmit = async (values) => {
        setLoading(true);
        console.log("user", user);
        const data = {
            ...values,
            requestorId: user.id,
            bookIds: selectedBooks
        };

        try {
            const response = await axios.post('/bookborrowingrequests', data, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            await message.success('Request created successfully');
            console.log('Request created:', response.data);
            window.location.href = '/';
        } catch (error) {
            console.log(error);
            message.error(error.response.data);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div style={formStyle}>
            <Title level={3} style={titleStyle}>Borrow Books</Title>
            <Divider />
            <Form {...formItemLayout} layout="vertical" onFinish={handleSubmit}>
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
                <Button type="primary" htmlType="submit" loading={loading} style={buttonStyle}>Borrow Books</Button>
            </Form>
        </div>
    );
};

export default BorrowBookForm;
