import React, { useEffect, useState } from 'react';
import { List, message, Typography, Spin, Card } from 'antd';
import axios from 'axios';
import { useAuth } from '../context/AuthContext';

const { Title, Text } = Typography;

const containerStyle = {
    padding: '24px',
    backgroundColor: '#fff',
    borderRadius: '4px',
    boxShadow: '0 2px 8px rgba(0, 0, 0, 0.15)',
};

const titleStyle = {
    textAlign: 'center',
    marginBottom: '24px',
};

const listItemStyle = {
    padding: '16px',
    borderRadius: '4px',
    boxShadow: '0 2px 4px rgba(0, 0, 0, 0.1)',
};

const UserRequests = () => {
    const { user, token } = useAuth();
    const [requests, setRequests] = useState([]);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        fetchUserRequests();
    }, []);

    const fetchUserRequests = async () => {
        setLoading(true);
        try {
            const response = await axios.get(`/bookborrowingrequests/userRequests/${user.id}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setRequests(response.data);
        } catch (error) {
            console.error('Error fetching user requests:', error);
            message.error('Failed to fetch user requests');
        } finally {
            setLoading(false);
        }
    };

    const renderItem = (item) => (
        <Card style={listItemStyle}>
            <Title level={4}>Request Date: {new Date(item.requestDate).toLocaleDateString()}</Title>
            <Text strong>Return Date: {new Date(item.bookBorrowingReturnDate).toLocaleDateString()}</Text>
            <br />
            <Text strong>Status: {getStatusText(item.status)}</Text>
            <br />
            <Text strong>Books:</Text>
            <ul>
                {item.bookBorrowingRequestDetails.map(detail => (
                    <li key={detail.bookId}>{detail.bookTitle}</li>
                ))}
            </ul>
        </Card>
    );

    const getStatusText = (status) => {
        switch (status) {
            case 0:
                return "Waiting";
            case 1:
                return "Approved";
            case 2:
                return "Rejected";
            default:
                return "Unknown";
        }
    };

    return (
        <div style={containerStyle}>
            <Title level={2} style={titleStyle}>Your Book Borrowing Requests</Title>
            {loading ? (
                <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '400px' }}>
                    <Spin size="large" />
                </div>
            ) : (
                <List
                    dataSource={requests}
                    renderItem={renderItem}
                />
            )}
        </div>
    );
};

export default UserRequests;