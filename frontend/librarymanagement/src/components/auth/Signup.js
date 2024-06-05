import React, { useState } from 'react';
import { Form, Input, Button, message } from 'antd';
import { useDropzone } from 'react-dropzone';
import axios from 'axios';

const Signup = () => {
    const [form] = Form.useForm();
    const [avatar, setAvatar] = useState(null);

    const { getRootProps, getInputProps } = useDropzone({
        accept: 'image/*',
        onDrop: (acceptedFiles) => {
            setAvatar(acceptedFiles[0]);
        },
    });

    const onFinish = async (values) => {
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

            console.log(formData);

            await axios.post('https://localhost:7049/api/auth/register', formData);
            message.success('Registration successful!');
            window.location.href = '/';
            // Redirect or perform any other necessary actions
        } catch (error) {
            console.error('Error during registration:', error.message);
            message.error('Registration failed. Please try again.');
        }
    };

    return (
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

            <Form.Item name="avatar" label="Avatar">
                <div {...getRootProps()}>
                    <input {...getInputProps()} />
                    <p>Drag and drop an image here, or click to select a file</p>
                </div>
            </Form.Item>

            <Form.Item>
                <Button type="primary" htmlType="submit" className="register-form-button">
                    Register
                </Button>
            </Form.Item>
        </Form>
    );
};

export default Signup;