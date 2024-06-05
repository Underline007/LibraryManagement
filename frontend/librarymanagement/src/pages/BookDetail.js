import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import { Spin, Card, Button } from 'antd';

const BookDetail = () => {
    const { id } = useParams();
    const [book, setBook] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchBook = async () => {
            try {
                const response = await axios.get(`https://localhost:7049/api/books/${id}`);
                setBook(response.data);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching book:', error);
            }
        };

        fetchBook();
    }, [id]);

    if (loading) {
        return <Spin />;
    }

    return (
        <div style={{ padding: '24px' }}>
            <Card
                title={book.title}
                style={{ width: '100%' }}
                cover={<img alt="Book" src={book.image} style={{ height: '400px', objectFit: 'cover' }} />}
            >
                <p><strong>Author:</strong> {book.author}</p>
                <p><strong>Description:</strong> {book.description}</p>
                <Button type="primary" href={`/books/${id}/edit`}>Edit</Button>
            </Card>
        </div>
    );
};

export default BookDetail;
