// NotAuthorized.js
import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Result, Button } from 'antd';

const NotAuthorized = () => {
    const navigate = useNavigate();

    useEffect(() => {
        const timer = setTimeout(() => {
            navigate('/');
        }, 3000);

        return () => clearTimeout(timer);
    }, [navigate]);

    return (
        <Result
            status="403"
            title="403"
            subTitle="Sorry, you are not authorized to access this page."
            extra={<Button type="primary" onClick={() => navigate('/')}>Go Home</Button>}
        />
    );
};

export default NotAuthorized;
