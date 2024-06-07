import React from 'react';
import { Layout, Row, Col } from 'antd';

const { Footer } = Layout;

const footerStyle = {
    backgroundColor: '#001529',
    color: '#ffffff',
    padding: '30px 0',
};

const columnStyle = {
    textAlign: 'center',
};

const linkStyle = {
    color: '#ffffff',
    textDecoration: 'none',
    marginRight: '10px',
};

const AppFooter = () => {
    return (
        <Footer style={footerStyle}>
            <Row>
                <Col span={12} style={columnStyle}>
                    <h3>About</h3>
                    <p>
                        Library Management is a comprehensive solution for managing your library's
                        collection, members, and operations.
                    </p>
                </Col>

                <Col span={12} style={columnStyle}>
                    <h3>Contact</h3>
                    <p>
                        123 Library Street <br />
                        City, State 12345 <br />
                        Phone: (123) 456-7890 <br />
                        Email: info@library.com
                    </p>
                </Col>
            </Row>
            <Row>
                <Col span={24} style={{ textAlign: 'center', marginTop: '20px' }}>
                    &copy; {new Date().getFullYear()} Library Management. All rights reserved.
                </Col>
            </Row>
        </Footer>
    );
};

export default AppFooter;