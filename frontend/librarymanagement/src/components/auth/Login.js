// src/components/Login.js
import React, { useState, useEffect } from 'react';
import { Form, Input, Button, message } from 'antd';
import { useAuth } from '../../context/AuthContext';

const Login = () => {
    const { login } = useAuth();
    const [loading, setLoading] = useState(false);

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
        <div style={{ maxWidth: 300, margin: 'auto', padding: '2rem' }}>
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
                    <Button type="primary" htmlType="submit" loading={loading}>
                        Login
                    </Button>
                </Form.Item>
            </Form>
        </div>
    );
};

export default Login;
