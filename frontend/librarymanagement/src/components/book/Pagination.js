import React, { useState } from 'react';
import { Button, InputNumber } from 'antd';
import { useBookContext } from "../../context/BookContext";

const Pagination = () => {
    const { pageNumber, totalPages, changePage, updatePageSize } = useBookContext();
    const [customPage, setCustomPage] = useState(pageNumber);
    const [customPageSize, setCustomPageSize] = useState(null);

    const handleCustomPageChange = () => {
        if (customPage >= 1 && customPage <= totalPages) {
            setTimeout(() => {
                changePage(customPage);
                updatePageSize(customPageSize); // Cập nhật kích thước trang sau khi thay đổi trang
            }); // Delay 1 giây trước khi thực hiện
        } else {
            // Xử lý khi trang nhập không hợp lệ, ví dụ: hiển thị thông báo lỗi
        }
    };

    const handleCustomPageSizeChange = (value) => {
        setCustomPageSize(value);
    };

    return (
        <div style={{ display: 'flex', justifyContent: 'center', margin: '20px 0' }}>
            <Button
                disabled={pageNumber === 1}
                onClick={() => changePage(pageNumber - 1)}
            >
                Previous
            </Button>
            <span style={{ margin: '0 10px' }}>
                Page {pageNumber} of {totalPages}
            </span>
            <Button
                disabled={pageNumber === totalPages}
                onClick={() => changePage(pageNumber + 1)}
            >
                Next
            </Button>
            <span style={{ margin: '0 10px', marginLeft: '20px' }}>
                Books per page: <InputNumber min={1} value={customPageSize} onChange={handleCustomPageSizeChange} />
            </span>
            <Button type="primary" onClick={handleCustomPageChange}>
                Go
            </Button>
        </div>
    );
};

export default Pagination;
