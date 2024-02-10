$(document).ready(function () {

    const setSortBySlValue = () => {
        let url = new window.URL(document.location);
        let param = url.searchParams.get("sortBy");
        if (param != null) {
            var value = $('.item-sortir').find('[data-option="' + param + '"]').text();
            $('.item-sortir .selected-value').text(value);
        }
    }

    const setItemsPerPageSlValue = () => {
        let url = new window.URL(document.location);
        let param = url.searchParams.get("itemsperpage");
        if (param != null)
            $('.item-options .selected-value').text(param);
    }

    const setPriceFilters = () => {
        let url = new window.URL(document.location);
        let minPriceParam = url.searchParams.get("minPrice");
        if (minPriceParam != null) {
            $('#minPrice').val(minPriceParam);
            $('#minPriceInput').val(minPriceParam);
        }
        let maxPriceParam = url.searchParams.get("maxPrice");
        if (maxPriceParam != null) {
            $('#maxPrice').val(maxPriceParam);
            $('#maxPriceInput').val(maxPriceParam);
        }
    }

    setSortBySlValue();

    setItemsPerPageSlValue();

    setPriceFilters();

    const updateFilters = () => {
        const searchParams = new URLSearchParams(window.location.search);

        $.ajax({
            url: '/catalog/updatefilters',
            type: 'get',
            traditional: true,
            data:
            {
                categoryId: searchParams.get('categoryId'),
                filters: searchParams.getAll('filters')
            },
            success: function (data) {
                $('.row.sidebar .filter').html(data);
            }
        });
    };

    const updateProducts = () => {
        const searchParams = new URLSearchParams(window.location.search);

        $.ajax({
            url: '/catalog/updateproducts',
            type: 'get',
            traditional: true,
            data:
            {
                categoryId: searchParams.get('categoryId'),
                page: searchParams.get('page'),
                itemsPerPage: searchParams.get('itemsPerPage'),
                minPrice: searchParams.get('minPrice'),
                maxPrice: searchParams.get('maxPrice'),
                sortBy: searchParams.get('sortBy'),
                filters: searchParams.getAll('filters')
            },
            success: function (data) {
                $('.products.main').html(data);
                updateFilters();
            }
        });
    };

    $('.item-sortir li').click(function () {
        let sortParam = $(this).data('option');
        if (!sortParam) sortParam = 0;

        let url = new window.URL(document.location);
        url.searchParams.set("sortBy", sortParam);
        window.history.pushState(null, null, url);
        updateProducts();
    });

    $('.item-options li').click(function () {
        let itemsPerPage = $(this).data('option');
        if (!itemsPerPage) itemsPerPage = 15;

        let url = new window.URL(document.location);
        url.searchParams.set("itemsperpage", itemsPerPage);
        window.history.pushState(null, null, url);
        updateProducts();
    });

    $('#minPriceInput, #minPrice').change(function () {
        let minPrice = parseInt($(this).val());
        if (!minPrice || minPrice < parseInt($(this).prop('min'))) minPrice = $(this).prop('min');

        let url = new window.URL(document.location);
        url.searchParams.set("minPrice", minPrice);
        window.history.pushState(null, null, url);
        updateProducts();
    });

    $('#maxPriceInput, #maxPrice').change(function () {
        let maxPrice = parseInt($(this).val());
        if (!maxPrice || maxPrice > parseInt($(this).prop('max'))) maxPrice = $(this).prop('max');

        let url = new window.URL(document.location);
        url.searchParams.set("maxPrice", maxPrice);
        window.history.pushState(null, null, url);
        updateProducts();
    });

    $('.filter').on('change', '.filter-block.params input', function () {
        let url = new window.URL(document.location);

        if ($(this).prop('checked'))
            url.searchParams.append("filters", $(this).prop('id'));
        else
            url.searchParams.delete("filters", $(this).prop('id'));

        window.history.pushState(null, null, url);
        updateProducts();
    });

    $('.filter').on('click', '.more-btn', function () {
        const searchParams = new URLSearchParams(window.location.search);
        const filterBlock = $(this).closest('.filter-block');

        $.ajax({
            url: '/catalog/updatefilterblock',
            type: 'get',
            traditional: true,
            data:
            {
                specificationTypeId: $(this).data('spec-type-id'),
                showAll: true,
                filters: searchParams.getAll('filters')
            },
            success: function (data) {
                $(filterBlock).replaceWith(data);
            }
        });
    });

    $('.filter').on('click', '.less-btn', function () {
        const searchParams = new URLSearchParams(window.location.search);
        const filterBlock = $(this).closest('.filter-block');

        $.ajax({
            url: '/catalog/updatefilterblock',
            type: 'get',
            traditional: true,
            data:
            {
                specificationTypeId: $(this).data('spec-type-id'),
                showAll: false,
                filters: searchParams.getAll('filters')
            },
            success: function (data) {
                $(filterBlock).replaceWith(data);
            }
        });
    });
});