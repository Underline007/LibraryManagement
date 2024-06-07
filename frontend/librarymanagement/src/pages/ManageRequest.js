import React, { useEffect, useState } from 'react';
import { Table, message, Select, Typography, Divider, Spin } from 'antd';
import axios from 'axios';
import { useAuth } from '../context/AuthContext';

const { Option } = Select;
const { Title } = Typography;

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

const ManageRequests = () => {
    const { token, user } = useAuth();
    const [requests, setRequests] = useState([]);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        fetchAllRequests();
    }, []);

    const fetchAllRequests = async () => {
        setLoading(true);
        try {
            const response = await axios.get('/bookborrowingrequests/manageRequests', {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            const updatedRequests = await Promise.all(
                response.data.map(async request => {
                    const requestorName = await fetchUserData(request.requestorId);

                    return {
                        ...request,
                        requestorName,
                    };
                })
            );

            setRequests(updatedRequests);
        } catch (error) {
            console.error('Error fetching all requests:', error);
            message.error('Failed to fetch all requests');
        } finally {
            setLoading(false);
        }
    };

    const fetchUserData = async (userId) => {
        try {
            const response = await axios.get(`/users/${userId}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            return response.data.fullName || 'Unknown';
        } catch (error) {
            console.error('Error fetching user data:', error);
            return 'Unknown';
        }
    };

    const handleStatusChange = async (requestId, newStatus, approverId) => {
        try {
            const requestToUpdate = requests.find(request => request.id === requestId);

            if (requestToUpdate.status === 1 || requestToUpdate.status === 2) {
                message.warning('Cannot update status for approved or rejected requests');
                return;
            }

            await axios.put(`/bookborrowingrequests/${requestId}/status?newStatus=${newStatus}&approverId=${approverId}`, {}, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            message.success('Request status updated successfully');
            fetchAllRequests();
        } catch (error) {
            console.error('Error updating request status:', error);
            message.error('Failed to update request status');
        }
    };

    const columns = [
        {
            title: 'Request Date',
            dataIndex: 'requestDate',
            key: 'requestDate',
            render: (text) => new Date(text).toLocaleDateString(),
        },
        {
            title: 'Return Date',
            dataIndex: 'bookBorrowingReturnDate',
            key: 'bookBorrowingReturnDate',
            render: (text) => new Date(text).toLocaleDateString(),
        },
        {
            title: 'Status',
            dataIndex: 'status',
            key: 'status',
            render: (text, record) => (
                <Select
                    defaultValue={text}
                    onChange={(value) => handleStatusChange(record.id, value, user.id)}
                    disabled={text === 1 || text === 2} // Disable select if already approved or rejected
                >
                    <Option value={0}>Waiting</Option>
                    <Option value={1}>Approved</Option>
                    <Option value={2}>Rejected</Option>
                </Select>
            ),
        },
        {
            title: 'Borrower Name',
            dataIndex: 'requestorName',
            key: 'requestorName',
        },
        {
            title: 'Books',
            dataIndex: 'bookBorrowingRequestDetails',
            key: 'books',
            render: (details) => (
                <ul>
                    {details.map(detail => (
                        <li key={detail.bookId}>{detail.bookTitle}</li>
                    ))}
                </ul>
            ),
        },
    ];

    return (
        <div style={containerStyle}>
            <Title level={3} style={titleStyle}>Manage Book Borrowing Requests</Title>
            <Divider />
            {loading ? (
                <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '400px' }}>
                    <Spin size="large" />
                </div>
            ) : (
                <Table
                    dataSource={requests}
                    columns={columns}
                    rowKey="id"
                    pagination={{ pageSize: 10 }}
                />
            )}
        </div>
    );
};

export default ManageRequests;
