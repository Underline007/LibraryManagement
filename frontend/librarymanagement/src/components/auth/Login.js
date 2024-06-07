import React, { useState, useEffect } from 'react';
import { Form, Input, Button, message, Typography, Card } from 'antd';
import { useAuth } from '../../context/AuthContext';

const { Title } = Typography;

const containerStyle = {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    height: '100vh',
    backgroundColor: '#f5f5f5',
};

const formContainerStyle = {
    padding: '24px',
    backgroundColor: '#fff',
    borderRadius: '4px',
    boxShadow: '0 2px 8px rgba(0, 0, 0, 0.15)',
    maxWidth: '400px',
    width: '100%',
};

const titleStyle = {
    textAlign: 'center',
    marginBottom: '24px',
};

const Login = () => {
    const { login } = useAuth();
    const [loading, setLoading] = useState(false);

    const handleSignUp = () => {
        window.location.href = '/signup';
    }

    const onFinish = async (values) => {
        try {
            setLoading(true);
            await login(values.email, values.password);
            message.success('Login successful');
            window.location.href = '/';
        } catch (error) {
            message.error('Invalid email or password');
            setLoading(false);
        }
    };

    return (
        <div style={containerStyle}>
            <Card style={formContainerStyle}>
                <Title level={3} style={titleStyle}>Login</Title>
                <Form name="login" onFinish={onFinish}>
                    <Form.Item
                        name="email"
                        rules={[{ required: true, message: 'Please input your email!' }]}
                    >
                        <Input placeholder="Email" />
                    </Form.Item>
                    <Form.Item
                        name="password"
                        rules={[{ required: true, message: 'Please input your password!' }]}
                    >
                        <Input.Password placeholder="Password" />
                    </Form.Item>
                    <Form.Item>
                        <Button type="primary" htmlType="submit" loading={loading} block>
                            Login
                        </Button>
                    </Form.Item>
                    <Typography onClick={handleSignUp}>Don't have an account? Register now</Typography>
                </Form>
            </Card>
        </div>
    );
};

export default Login;