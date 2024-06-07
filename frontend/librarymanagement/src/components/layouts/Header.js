import React from 'react';
import { Layout, Menu, Space, Avatar, Typography } from 'antd';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import { BookOutlined, UserOutlined, LogoutOutlined, HomeOutlined, LoginOutlined, TagsOutlined, SolutionOutlined } from '@ant-design/icons';

const { Header } = Layout;
const { Text } = Typography;

const AppHeader = () => {
    const { token, user, logout } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        window.location.href = '/login';
    };

    const handleNewBorrowingRequests = () => {
        navigate(`/borrowing-book`);
    };

    const handleYourBorrowingRequests = () => {
        navigate(`/manage-request/${user.id}`);
    };

    return (
        <Header style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', padding: '0 20px' }}>
            <div className="logo" style={{ display: 'flex', alignItems: 'center' }}>
                <BookOutlined style={{ fontSize: '24px', color: '#fff', marginRight: '10px' }} />
                <Text strong style={{ color: '#fff', fontSize: '18px' }}>Library Web App</Text>
            </div>
            <Menu theme="dark" mode="horizontal" defaultSelectedKeys={['1']} style={{ lineHeight: '64px', flex: 1, marginLeft: "5px" }}>
                <Menu.Item key="1" icon={<HomeOutlined />}>
                    <Link to="/">Home</Link>
                </Menu.Item>
                {token ? (
                    <>
                        {user.role === 0 && (
                            <>
                                <Menu.Item key="2" icon={<SolutionOutlined />} onClick={handleNewBorrowingRequests}>
                                    <Link to="/borrowing-book">New Borrowing Request</Link>
                                </Menu.Item>
                                <Menu.Item key="3" icon={<SolutionOutlined />} onClick={handleYourBorrowingRequests}>
                                    Your Borrowing Requests
                                </Menu.Item>
                            </>
                        )}
                        {user.role === 1 && (
                            <>
                                <Menu.Item key="6" icon={<BookOutlined />}>
                                    <Link to="/manage-book">Manage Books</Link>
                                </Menu.Item>
                                <Menu.Item key="7" icon={<TagsOutlined />}>
                                    <Link to="/categories">Manage Categories</Link>
                                </Menu.Item>
                                <Menu.Item key="8" icon={<SolutionOutlined />}>
                                    <Link to="/manage-request">Manage Requests</Link>
                                </Menu.Item>
                            </>
                        )}
                    </>
                ) : (
                    <Space size="middle" style={{ marginLeft: 'auto' }}>
                        <Menu.Item key="4" icon={<LoginOutlined />}>
                            <Link to="/login">Login</Link>
                        </Menu.Item>
                    </Space>
                )}
            </Menu>
            {token && (
                <Space size="middle" style={{ marginLeft: 'auto' }}>
                    <Avatar size="large" icon={<UserOutlined />} src={user.avatar} />
                    <Text style={{ color: '#fff' }}>Hello, {user.fullName}</Text>
                    <Menu theme="dark" mode="horizontal" selectable={false}>
                        <Menu.Item key="logout" icon={<LogoutOutlined />} onClick={handleLogout}>
                            Logout
                        </Menu.Item>
                    </Menu>
                </Space>
            )}
        </Header>
    );
};

export default AppHeader;