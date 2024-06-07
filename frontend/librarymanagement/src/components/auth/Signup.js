import React, { useState } from 'react';
import { Form, Input, Button, message, Typography, Card, Divider } from 'antd';
import { useDropzone } from 'react-dropzone';
import axios from 'axios';

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
    maxWidth: '500px',
    width: '100%',
};

const titleStyle = {
    textAlign: 'center',
    marginBottom: '24px',
};

const dropzoneStyle = {
    border: '2px dashed #d9d9d9',
    borderRadius: '4px',
    padding: '20px',
    textAlign: 'center',
    cursor: 'pointer',
};

const Signup = () => {
    const [form] = Form.useForm();
    const [avatar, setAvatar] = useState(null);
    const [loading, setLoading] = useState(false); // State to manage loading

    const { getRootProps, getInputProps } = useDropzone({
        accept: 'image/*',
        onDrop: (acceptedFiles) => {
            setAvatar(acceptedFiles[0]);
        },
    });

    const onFinish = async (values) => {
        setLoading(true);
        try {
            const formData = new FormData();
            formData.append('Email', values.email);
            formData.append('Password', values.password);
            formData.append('FullName', values.fullName);
            formData.append('Address', values.address);
            formData.append('Username', values.username);
            if (avatar) {
                formData.append('Avatar', avatar);
            }

            await axios.post('/auth/register', formData);
            message.success('Registration successful!, Please login to website');
            window.location.href = '/';
        } catch (error) {
            console.error('Error during registration:', error.message);
            message.error('Registration failed. Please try again.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div style={containerStyle}>
            <Card style={formContainerStyle}>
                <Title level={3} style={titleStyle}>Register</Title>
                <Divider />
                <Form form={form} name="register_form" onFinish={onFinish} autoComplete="off">
                    <Form.Item
                        name="email"
                        label="Email"
                        rules={[{ required: true, message: 'Please input your email!' }]}
                    >
                        <Input />
                    </Form.Item>

                    <Form.Item
                        name="password"
                        label="Password"
                        rules={[{ required: true, message: 'Please input your password!' }]}
                    >
                        <Input.Password />
                    </Form.Item>

                    <Form.Item
                        name="fullName"
                        label="Full Name"
                        rules={[{ required: true, message: 'Please input your full name!' }]}
                    >
                        <Input />
                    </Form.Item>

                    <Form.Item
                        name="address"
                        label="Address"
                        rules={[{ required: true, message: 'Please input your address!' }]}
                    >
                        <Input />
                    </Form.Item>

                    <Form.Item
                        name="username"
                        label="Username"
                        rules={[{ required: true, message: 'Please input your username!' }]}
                    >
                        <Input />
                    </Form.Item>

                    <Form.Item label="Upload Image">
                        <div {...getRootProps()} style={dropzoneStyle}>
                            <input {...getInputProps()} />
                            <p>Drag 'n' drop an image here, or click to select an image</p>
                        </div>
                        {avatar && <p style={{ textAlign: 'center', marginTop: '10px' }}>{avatar.name}</p>}
                    </Form.Item>

                    <Form.Item>
                        <Button type="primary" htmlType="submit" className="register-form-button" block loading={loading}>
                            Register
                        </Button>
                    </Form.Item>
                </Form>
            </Card>
        </div>
    );
};

export default Signup;
