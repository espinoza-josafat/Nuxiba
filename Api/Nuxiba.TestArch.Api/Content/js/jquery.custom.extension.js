function AjaxGetNoCacheJsonAsync(url, successFunction, errorFunction, beforeSendFunction, completeFunction) {
	$.ajax({
		url: url,
		cache: false,
		dataType: "json",
		type: "GET",
		beforeSend: (beforeSendFunction && typeof (beforeSendFunction) === "function") ? beforeSendFunction : function () {
			if (mostrarOverlayLoading && typeof (mostrarOverlayLoading) === "function")
				mostrarOverlayLoading(true);
		},
		success: (successFunction && typeof (successFunction) === "function") ? successFunction : function (data) {

		},
		error: (errorFunction && typeof (errorFunction) === "function") ? errorFunction : function (data) {
			if (data && data.responseJSON && data.responseJSON.message && mostrarModalMensaje && typeof (mostrarModalMensaje) === "function")
				mostrarModalMensaje(2, "Book Management System", data.responseJSON.message);
		},
		complete: (completeFunction && typeof (completeFunction) === "function") ? completeFunction : function () {
			if (mostrarOverlayLoading && typeof (mostrarOverlayLoading) === "function")
				mostrarOverlayLoading(false);
		}
	});
}

function AjaxPostNoCacheJsonAsync(url, dataJson, successFunction, errorFunction, beforeSendFunction, completeFunction) {
	$.ajax({
		url: url,
		cache: false,
		data: dataJson ? JSON.stringify(dataJson) : JSON.stringify({}),
		dataType: "json",
		type: "POST",
		contentType: "application/json; charset=utf-8",
		beforeSend: (beforeSendFunction && typeof (beforeSendFunction) === "function") ? beforeSendFunction : function () {
			if (mostrarOverlayLoading && typeof (mostrarOverlayLoading) === "function")
				mostrarOverlayLoading(true);
		},
		success: (successFunction && typeof (successFunction) === "function") ? successFunction : function (data) {

		},
		error: (errorFunction && typeof (errorFunction) === "function") ? errorFunction : function (data) {
			if (data && data.responseJSON && data.responseJSON.message && mostrarModalMensaje && typeof (mostrarModalMensaje) === "function")
				mostrarModalMensaje(2, "Book Management System", data.responseJSON.message);
		},
		complete: (completeFunction && typeof (completeFunction) === "function") ? completeFunction : function () {
			if (mostrarOverlayLoading && typeof (mostrarOverlayLoading) === "function")
				mostrarOverlayLoading(false);
		}
	});
}