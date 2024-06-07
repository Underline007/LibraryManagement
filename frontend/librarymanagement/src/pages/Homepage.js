import React, { useEffect } from 'react';
import { Input, Select, Card, Row, Col, Spin } from 'antd';
import { useNavigate } from 'react-router-dom';
import { useBookContext } from '../context/BookContext';
import { useCategoryContext } from '../context/CategoryContext';
import Pagination from "../components/book/Pagination";

const { Search } = Input;
const { Option } = Select;
const { Meta } = Card;

const HomePage = () => {
    const {
        books,
        loading,
        pageNumber,
        pageSize,
        totalPages,
        changePage,
        updatePageSize,
        setSearchTerm,
        setSortBy,
        fetchBooksByQuery,
        searchTerm,
        sortBy
    } = useBookContext();
    const { categories } = useCategoryContext();
    const navigate = useNavigate();

    useEffect(() => {
        fetchBooksByQuery();
    }, [searchTerm, sortBy]);

    const handleSearch = value => {
        setSearchTerm(value);
    };

    const handleSortChange = value => {
        setSortBy(value);
    };

    const getCategoryName = (categoryId) => {
        const category = categories.find(category => category.id === categoryId);
        return category ? category.name : 'Unknown';
    };

    const handleCardClick = (bookId) => {
        navigate(`/books/${bookId}`);
    };

    const cardStyle = {
        marginBottom: '20px',
        transition: 'transform 0.3s ease'
    };

    const cardHoverStyle = {
        transform: 'scale(0.95)'
    };

    return (
        <div style={{ padding: '20px' }}>
            <h1>Book Library</h1>
            <div style={{ display: 'flex', marginBottom: '20px' }}>
                <Search
                    placeholder="Search books"
                    enterButton="Search"
                    size="large"
                    onSearch={handleSearch}
                    style={{ marginRight: '10px' }}
                />
                <Select
                    defaultValue="title"
                    size="large"
                    onChange={handleSortChange}
                    style={{ width: '200px' }}
                >
                    <Option value="title">Sort by Title</Option>
                    <Option value="author">Sort by Author</Option>
                </Select>
            </div>
            {loading ? (
                <Spin size="large" />
            ) : (
                <>
                    <Row gutter={[16, 16]}>
                        {books.map(book => (
                            <Col key={book.id} xs={24} sm={12} md={8} lg={6} xl={4}>
                                <div
                                    style={cardStyle}
                                    onMouseEnter={(e) => e.currentTarget.style.transform = cardHoverStyle.transform}
                                    onMouseLeave={(e) => e.currentTarget.style.transform = 'scale(1)'}
                                    onClick={() => handleCardClick(book.id)}
                                >
                                    <Card
                                        hoverable
                                        cover={<img alt={book.title} src={book.image} />}
                                    >
                                        <Meta title={book.title} description={`Author: ${book.author}`} />
                                        <p>Category: {getCategoryName(book.categoryId)}</p>
                                    </Card>
                                </div>
                            </Col>
                        ))}
                    </Row>
                    <Pagination />
                </>
            )}
        </div>
    );
};

export default HomePage;
