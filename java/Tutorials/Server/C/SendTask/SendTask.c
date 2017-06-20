#include <TutorialUtils.h>

#include <NCore.h>
#include <NCluster.h>
#include <NBiometrics.h>

const NChar title[] = N_T("SendTask");
const NChar description[] = N_T("Demonstrates how to send a task to matching server and wait for result");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2008-2017 Neurotechnology");

const char defaultServerIp[] = "127.0.0.1";
const char defaultServerPort[] = "25452";
const char defaultQuery[] = "SELECT node_id, dbid FROM node_tbl";

int usage()
{
	printf("usage:\n");
	printf("\t%s -s [server:port] -t [template] -q [query] -y [template type]\n", title);
	printf("\t%s -s [server:port] -t [template] -q [query]\n", title);
	printf("\n");
	printf("\t-s server:port   - matching server address (optional parameter, if address specified - port is optional)\n");
	printf("\t-t template      - template to be sent for matching (required)\n");
	printf("\t-q query         - database query to execute (optional)\n");
	printf("\t-y template type - template type (optional - specify parameter only if template is not NTemplate, but FMRecord ansi or iso). Parameter values: ansi or iso.\n");
	printf("\tImportant! Template type parameter is available only for Accelerator product!\n");
	printf("default query (if not specified): %s\n", defaultQuery);

	return 1;
}

#define CHECKOPT() \
{ \
	if (optarg == NULL) \
{ \
	parse_error = 1; \
	goto breakloop; \
} \
	c++; \
}

/**
* Parses arguments from command line.
*/
static int
parse_args(int argc, char **argv, char **srv_ip, char **srv_port, char **template, char **query
		, int * isStandardTemplate, cluster_standard_template_type_e *templateType
		)
{
	int c;
	int parse_error = 0;
	char *separator;

	if (argv == NULL || srv_ip == NULL || srv_port == NULL || template == NULL ||
		query == NULL || templateType == NULL || isStandardTemplate == NULL
		)
	{
		return CLUSTER_ARGUMENT_ERROR;
	}
	
	*srv_ip = (char*) malloc(strlen(defaultServerIp) + 1);
	strncpy(*srv_ip, defaultServerIp, strlen(defaultServerIp));
	(*srv_ip)[strlen(defaultServerIp)] = '\0';

	*srv_port = (char*) malloc(strlen(defaultServerPort) + 1);
	strncpy(*srv_port, defaultServerPort, strlen(defaultServerPort));
	(*srv_port)[strlen(defaultServerPort)] = '\0';

	*query = (char*)defaultQuery;

	*isStandardTemplate = 0;
	*templateType = CLUSTER_TEMPLATE_ANSI;

	*template = NULL;

	for (c = 1; c < argc; c++)
	{
		char *optarg = NULL;

		if (strlen(argv[c]) != 2 || argv[c][0] != '-')
		{
			parse_error = 1;
			break;
		}

		if (argc > c+1 && argv[c+1][0] != '-')
		{
			optarg = argv[c+1]; // we have a parameter for given flag
		}

		switch (argv[c][1])
		{
			case 's':
				CHECKOPT();
				separator = strchr(optarg, ':');
				if (separator)
				{
					size_t addressLength;
					size_t portLength;

					addressLength = separator - optarg;
					portLength = strlen(optarg) - addressLength - 1;
					*srv_ip = (char*) malloc(addressLength + 1);
					strncpy(*srv_ip, optarg, addressLength);
					(*srv_ip)[addressLength] = '\0';
					*srv_port = (char*) malloc(portLength + 1);
					strncpy(*srv_port, optarg + addressLength + 1, portLength);
					(*srv_port)[portLength] = '\0';
				}
				else
				{
					*srv_ip = (char*) malloc(strlen(optarg) + 1);
					strncpy(*srv_ip, optarg, strlen(optarg));
					(*srv_ip)[strlen(optarg)] = '\0';
				}
				break;
			case 't':
				CHECKOPT();
				*template = optarg;
				break;
			case 'q':
				CHECKOPT();
				*query = optarg;
				break;
			case 'y':
				CHECKOPT();
				if (_stricmp(optarg, N_T("ansi")) == 0)
				{
					*templateType = CLUSTER_TEMPLATE_ANSI;
					*isStandardTemplate = 1;
				}
				else if (_stricmp(optarg, N_T("iso")) == 0)
				{
					*templateType = CLUSTER_TEMPLATE_ISO;
					*isStandardTemplate = 1;
				}
				else
				{
					printf(N_T("wrong standard (should be iso or ansi)!"));
					parse_error = 1;
					goto breakloop;
				}
				break;
			default:
				parse_error = 1;
				goto breakloop;
		}

	} /*   while   */

breakloop:

	if (*template == NULL)
	{
		printf(N_T("template is not specified!"));
		parse_error = 1;
	}

	if (parse_error)
	{
		usage();
		return CLUSTER_ARGUMENT_ERROR;
	}

	return CLUSTER_OK;
}

int freeResources()
{
	cluster_status_code_e code = cluster_finalize();
	if (code != CLUSTER_OK)
	{
		printf("error in cluster_initialize, error code: %d\n", code);
	}
	return code;
}

int closeClientHandle(client_handle_t *clientHandle)
{
	cluster_status_code_e code = finalize_client_handle(clientHandle);
	if (code != CLUSTER_OK)
	{
		printf("error in finalize_client_handle, error code: %d\n", code);
	}
	return code;
}

int receiveTaskID(cluster_socket_t *srvSocket)
{
	client_handle_t *clientHandle = NULL;
	cluster_status_code_e code = 0;

	int taskID = 0;

	clientHandle = initialize_client_handle();
	if (!clientHandle)
	{
		printf("error in initialize_client_handle\n");
		return -1;
	}

	code = cluster_packet_recv(clientHandle, srvSocket);
	if (code != CLUSTER_OK)
	{
		printf("error in cluster_packet_recv, code: %d\n", code);
		closeClientHandle(clientHandle);
		return -1;
	}

	code = cluster_get_task_id(clientHandle, &taskID);
	if (code != CLUSTER_OK)
	{
		printf("error in cluster_get_task_id, code: %d\n", code);
		closeClientHandle(clientHandle);
		return -1;
	}

	if (closeClientHandle(clientHandle))
		return -1;

	return taskID;
}

int receiveProgess(cluster_socket_t *srvSocket, int *progress, int *count)
{
	client_handle_t *clientHandle = NULL;
	cluster_status_code_e code = 0;

	int progressZ = 0;
	int matchedCount = 0;

	clientHandle = initialize_client_handle();
	if (!clientHandle)
	{
		printf("error in initialize_client_handle\n");
		return -1;
	}

	code = cluster_packet_recv(clientHandle, srvSocket);
	if (code != CLUSTER_OK)
	{
		printf("error in cluster_packet_recv, code: %d\n", code);
		closeClientHandle(clientHandle);
		return -1;
	}

	code = cluster_get_task_progress(clientHandle, &progressZ, &matchedCount);
	if (code != CLUSTER_OK)
	{
		printf("error in cluster_get_task_progresss, code: %d\n", code);
		closeClientHandle(clientHandle);
		return -1;
	}

	if (closeClientHandle(clientHandle))
		return -1;

	*progress = progressZ;
	*count = matchedCount;

	return 0;
}

int receiveAndPrintResults(cluster_socket_t *srvSocket)
{
	client_handle_t *clientHandle = NULL;
	cluster_status_code_e code = 0;
	int i;
	int id_len, md_len;
	char *idStr = 0;
	void *mdStr = 0;
	int count = 0;
	int status = 0;

	clientHandle = initialize_client_handle();
	if (!clientHandle)
	{
		printf("error in initialize_client_handle\n");
		return -1;
	}

	code = cluster_packet_recv(clientHandle, srvSocket);
	if (code != CLUSTER_OK)
	{
		printf("error in cluster_packet_recv, code: %d\n", code);
		closeClientHandle(clientHandle);
		return -1;
	}

	code = cluster_get_results_status(clientHandle, &status);
	if (code != CLUSTER_OK)
	{
		printf("error in cluster_get_results_status, code: %d\n", code);
		closeClientHandle(clientHandle);
		return -1;
	}

	code = cluster_get_results_count(clientHandle, &count);
	if (code != CLUSTER_OK)
	{
		printf("error in cluster_get_results_count, code: %d\n", code);
		closeClientHandle(clientHandle);
		return -1;
	}

	for (i = 0; i < count; i++)
	{
		int score;

		code = cluster_get_results_info(clientHandle, i, &id_len, &md_len);
		if (code != CLUSTER_OK)
		{
			printf("error in cluster_get_results_info, code: %d\n", code);
			closeClientHandle(clientHandle);
			return code;
		}

		idStr = (char *) calloc(id_len + 1, sizeof(char));
		mdStr = (void *) calloc(md_len, sizeof(unsigned char));

		code = cluster_get_results_similarity(clientHandle, i, id_len, idStr, &score);
		if (code != CLUSTER_OK)
		{
			printf("error in cluster_get_results_data, code: %d\n", code);
			closeClientHandle(clientHandle);
			return code;
		}

		printf("... matched with id: %s, score: %d\n", idStr, score);

		free(idStr);
		free(mdStr);
	}

	return closeClientHandle(clientHandle);
}

int sendTaskStandardTemplate(cluster_socket_t *srvSocket, void *nTemplateBuffer, NSizeType nTemplateBufferSize, cluster_standard_template_type_e templateType, const char* query, int *taskNumber)
{
	cluster_status_code_e code;
	client_handle_t *clientHandle = NULL;
	cluster_task_params_t *cluster_task_params;
	int taskID = 0;
	//int fingerprintMatchingFAR;
	//NByte fingerprintMaximalRotation;

	clientHandle = initialize_client_handle();
	if (!clientHandle)
	{
		printf("error in initialize_client_handle\n");
		closeClientHandle(clientHandle);
		return -1;
	}
	
	//fingerprintMatchingFAR = 60;
	//fingerprintMaximalRotation = 5;

	cluster_task_params = cluster_task_params_create();
	//cluster_task_params_add_param(cluster_task_params, 0, NMP_FINGERS_MAXIMAL_ROTATION, fingerprintMaximalRotation);
	//cluster_task_params_add_param(cluster_task_params, 0, NMP_MATCHING_THRESHOLD, fingerprintMatchingFAR);

	code = cluster_packet_create_standard_template_task(clientHandle, CLUSTER_NORMAL, (int)nTemplateBufferSize, nTemplateBuffer, templateType,
		(int)strlen(query), query,
		cluster_task_params_get_param_buffer_size(cluster_task_params),
		cluster_task_params_get_param_buffer(cluster_task_params), 100);

	cluster_task_params_free(cluster_task_params);

	if (code != CLUSTER_OK)
	{
		printf("error in cluster_packet_create_task, error code: %d\n", code);
		closeClientHandle(clientHandle);
		return code;
	}

	code = cluster_packet_send(clientHandle, srvSocket);
	if (code != CLUSTER_OK)
	{
		printf("error in cluster_packet_send, error code: %d\n", code);
		closeClientHandle(clientHandle);
		return code;
	}

	taskID = receiveTaskID(srvSocket);
	if (taskID == -1)
	{
		closeClientHandle(clientHandle);
		return taskID;
	}
	*taskNumber = taskID;
	printf("... task started, task ID = %d\n", taskID);

	return closeClientHandle(clientHandle);
}

int sendTask(cluster_socket_t *srvSocket, void *nTemplateBuffer, NSizeType nTemplateBufferSize, const char* query, int *taskNumber)
{
	cluster_status_code_e code;
	client_handle_t *clientHandle = NULL;
	cluster_task_params_t *cluster_task_params;
	int taskID = 0;
	//int fingerprintMatchingFAR;
	//NByte fingerprintMaximalRotation;

	clientHandle = initialize_client_handle();
	if (!clientHandle)
	{
		printf("error in initialize_client_handle\n");
		closeClientHandle(clientHandle);
		return -1;
	}
	
	//fingerprintMatchingFAR = 60;
	//fingerprintMaximalRotation = 5;

	cluster_task_params = cluster_task_params_create();
	//cluster_task_params_add_param(cluster_task_params, 0, NMP_FINGERS_MAXIMAL_ROTATION, fingerprintMaximalRotation);
	//cluster_task_params_add_param(cluster_task_params, 0, NMP_MATCHING_THRESHOLD, fingerprintMatchingFAR);

	code = cluster_packet_create_task(clientHandle, CLUSTER_NORMAL, (int)nTemplateBufferSize, nTemplateBuffer,
		(int)strlen(query), query,
		cluster_task_params_get_param_buffer_size(cluster_task_params),
		cluster_task_params_get_param_buffer(cluster_task_params), 100);

	cluster_task_params_free(cluster_task_params);

	if (code != CLUSTER_OK)
	{
		printf("error in cluster_packet_create_task, error code: %d\n", code);
		closeClientHandle(clientHandle);
		return code;
	}

	code = cluster_packet_send(clientHandle, srvSocket);
	if (code != CLUSTER_OK)
	{
		printf("error in cluster_packet_send, error code: %d\n", code);
		closeClientHandle(clientHandle);
		return code;
	}

	taskID = receiveTaskID(srvSocket);
	if (taskID == -1)
	{
		closeClientHandle(clientHandle);
		return taskID;
	}
	*taskNumber = taskID;
	printf("... task started, task ID = %d\n", taskID);

	return closeClientHandle(clientHandle);
}

int receiveResults(cluster_socket_t *srvSocket, int taskID)
{
	int progress = 0;
	int count = 0;
	int result = 0;
	cluster_status_code_e code;
	client_handle_t *clientHandle;

	printf("waiting for results ...\n");
	do
	{
		clientHandle = initialize_client_handle();
		if (!clientHandle)
		{
			printf("error in initialize_client_handle\n");
			closeClientHandle(clientHandle);
			return -1;
		}

		code = cluster_packet_create_progress_request(clientHandle, taskID);
		if (code != CLUSTER_OK)
		{
			printf("error in cluster_packet_create_progress_request, error code: %d\n", code);
			closeClientHandle(clientHandle);
			return code;
		}

		code = cluster_packet_send(clientHandle, srvSocket);
		if (code != CLUSTER_OK)
		{
			printf("error in cluster_packet_send, error code: %d\n", code);
			closeClientHandle(clientHandle);
			return code;
		}
		
		result = receiveProgess(srvSocket, &progress, &count);
		if (result)
		{
			closeClientHandle(clientHandle);
			return result;
		}

		closeClientHandle(clientHandle);

		printf("progress: %d\n", progress);

		if (progress != 100)
		{
			NThreadSleep(100);
		}

		if (progress < 0)
		{
			printf("task aborted on server side.\n");
			return -1;
		}
	} while (progress != 100);
	
	if (count > 0)
	{
		clientHandle = initialize_client_handle();
		if (!clientHandle)
		{
			printf("error in initialize_client_handle\n");
			closeClientHandle(clientHandle);
			return -1;
		}

		code = cluster_packet_create_result_request(clientHandle, taskID, 1, count);
		if (code != CLUSTER_OK)
		{
			printf("error in cluster_packet_create_result_request, error code: %d\n", code);
			closeClientHandle(clientHandle);
			return code;
		}

		code = cluster_packet_send(clientHandle, srvSocket);
		if (code != CLUSTER_OK)
		{
			printf("error in cluster_packet_send, error code: %d\n", code);
			closeClientHandle(clientHandle);
			return code;
		}

		result = receiveAndPrintResults(srvSocket);
		if (result)
		{
			closeClientHandle(clientHandle);
			return result;
		}

		closeClientHandle(clientHandle);
	}
	else
	{
		printf("... no matches\n");
	}

	/* Free information about tasks here */
	clientHandle = initialize_client_handle();
	if (!clientHandle)
	{
		printf("error in initialize_client_handle\n");
		closeClientHandle(clientHandle);
		return -1;
	}

	code = cluster_packet_create_result_delete(clientHandle, taskID);
	if (code != CLUSTER_OK)
	{
		printf("error in cluster_packet_create_result_delete, error code: %d\n", code);
		closeClientHandle(clientHandle);
		return code;
	}

	code = cluster_packet_send(clientHandle, srvSocket);
	if (code != CLUSTER_OK)
	{
		printf("error in cluster_packet_send, error code: %d\n", code);
		closeClientHandle(clientHandle);
		return code;
	}

	return closeClientHandle(clientHandle);
}

int main(int argc, char **argv)
{
	char *server = NULL;
	char *port = NULL;
	char *fileName = NULL;
	cluster_status_code_e code;
	cluster_socket_t *serverSocket = NULL;
	int resultCode = 0;
	int taskID = 0;
	int isStandartTemplate = 0;
	cluster_standard_template_type_e templateType;
	HNBuffer hBuffer = NULL;
	void * pTemplateBuffer = NULL;
	NSizeType templateBufferSize = 0;

	NResult res = N_OK;
	char* query = NULL;

	OnStart(title, description, version, copyright, argc, argv);

	if(argc < 2)
	{
		OnExit();
		return usage();
	}

	res = parse_args(argc, argv, &server, &port, &fileName, &query, &isStandartTemplate, &templateType);
	if (res != CLUSTER_OK)
	{
		printf("could not parse args\n");
		res = N_E_FAILED;
		goto FINALLY;
	}

	/* read template */
	res = NFileReadAllBytesCN(fileName, &hBuffer);
	if (NFailed(res))
	{
		printf("failed to read template from file: %d\n", res);
		goto FINALLY;
	}

	res = NBufferGetPtr(hBuffer, &pTemplateBuffer);
	if (NFailed(res))
	{
		printf("failed to get template buffer ptr: %d\n", res);
		goto FINALLY;
	}

	res = NBufferGetSize(hBuffer, &templateBufferSize);
	if (NFailed(res))
	{
		printf("failed to get template buffer size: %d\n", res);
		goto FINALLY;
	}

	code = cluster_initialize();
	if (code != CLUSTER_OK)
	{
		printf("error in cluster_initialize, error code: %d\n", code);
		res = code;
		goto FINALLY;
	}

	serverSocket = cluster_sock_connect(&code, server, port);
	if (code != CLUSTER_OK)
	{
		printf("error in cluster_sock_connect, error code: %d\n", code);
		res = code;
		goto FINALLY;
	}

	/* Sending task */
	if (isStandartTemplate == 0)
	{
		resultCode = sendTask(serverSocket, pTemplateBuffer, templateBufferSize, query, &taskID);
	}
	else
	{
		resultCode = sendTaskStandardTemplate(serverSocket, pTemplateBuffer, templateBufferSize, templateType, query, &taskID);
	}
	if (resultCode)
	{
		printf("failed to send task, error code: %d\n", resultCode);
		res = resultCode;
		goto FINALLY;
	}

	/* Receive the results here */
	resultCode = receiveResults(serverSocket, taskID);
	if (resultCode)
	{
		printf("failed to receive results, error code: %d\n", resultCode);
		res = resultCode;
		goto FINALLY;
	}

FINALLY:
	if (server)
		free(server);
	if (port)
		free(port);

	if (serverSocket != NULL)
	{
		code = cluster_sock_close(serverSocket);
		if (code != CLUSTER_OK)
		{
			printf("error in cluster_sock_close, error code: %d\n", code);
		}
	}

	NObjectSet(NULL, &hBuffer);

	freeResources();

	OnExit();

	return res;
}
