import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { Spin, Card, Button, Typography, Divider } from 'antd';

const { Meta } = Card;
const { Title, Text } = Typography;

const containerStyle = {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    minHeight: '100vh',
    backgroundColor: '#f0f2f5',
    padding: '24px',
};

const cardStyle = {
    width: '80%',
    boxShadow: '0 4px 8px 0 rgba(0, 0, 0, 0.2)',
    borderRadius: '8px',
};

const coverStyle = {
    height: '400px',
    objectFit: 'cover',
    borderTopLeftRadius: '8px',
    borderTopRightRadius: '8px',
};

const metaStyle = {
    padding: '24px',
};

const descriptionStyle = {
    marginBottom: '16px',
};

const BookDetail = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [book, setBook] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchBook = async () => {
            try {
                const response = await axios.get(`/books/${id}`);
                setBook(response.data);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching book:', error);
            }
        };

        fetchBook();
    }, [id]);

    if (loading) {
        return (
            <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
                <Spin size="large" />
            </div>
        );
    }

    const handleGoBack = () => {
        navigate(-1);
    };

    return (
        <div style={containerStyle}>
            <Card style={cardStyle} cover={<img alt="Book" src={book.image} style={coverStyle} />}>
                <Meta style={metaStyle} title={<Title level={3}>{book.title}</Title>} />
                <Divider />
                <div style={descriptionStyle}>
                    <Text strong>Author:</Text> {book.author}
                </div>
                <div style={descriptionStyle}>
                    <Text strong>Description:</Text> {book.description}
                </div>
                <Button type="primary" onClick={handleGoBack}>
                    Back to home
                </Button>
            </Card>
        </div>
    );
};

export default BookDetail;