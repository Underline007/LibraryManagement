import React from 'react';
import { Layout, Menu } from 'antd';
import { Link } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';

const { Header } = Layout;

const AppHeader = () => {
    const { token, user, logout } = useAuth();

    const handleLogout = () => {
        logout();
        window.location.href = '/login';
    };

    return (
        <Header>
            <div className="logo" />
            <Menu theme="dark" mode="horizontal" defaultSelectedKeys={['1']}>
                <Menu.Item key="1">
                    <Link to="/">Home</Link>
                </Menu.Item>
                <Menu.Item key="2">
                    <Link to="/about">About</Link>
                </Menu.Item>
                <Menu.Item key="3">
                    <Link to="/categories">Category</Link>
                </Menu.Item>
                {token ? (
                    <>
                        <Menu.Item key="4" onClick={handleLogout}>
                            Logout
                        </Menu.Item>

                        <Menu.Item key="5">
                            Hello, {user.fullName}
                        </Menu.Item>

                    </>
                ) : (
                    <Menu.Item key="4">
                        <Link to="/login">Login</Link>
                    </Menu.Item>
                )}
            </Menu>
        </Header>
    );
};

export default AppHeader;
